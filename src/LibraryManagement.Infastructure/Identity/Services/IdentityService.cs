using ErrorOr;
using LibraryManagement.Application.Auth.Common;
using LibraryManagement.Application.Common.Interface;
using LibraryManagement.Domain.Common.Enums;
using LibraryManagement.Domain.Common.Interface;
using LibraryManagement.Domain.PatronAggregate;
using LibraryManagement.Infastructure.Data.Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using LibraryManagement.Application.Auth.ListAccount;
using MimeKit;

namespace LibraryManagement.Infastructure.Data.Identity.Services
{
	public class IdentityService : IIdentityService
	{
		private readonly UserManager<User> _userManager;
		private readonly IUserClaimsPrincipalFactory<User> _userClaimsPrincipalFactory;
		private readonly IAuthorizationService _authorizationService;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly IDateTimeProvider _datetimeProvider;
		private readonly ITokenGennerator _tokenGennerator;
		private readonly IBaseRepository<Patron> _patronRepository;
		private readonly IEmailService _emailService;

		public IdentityService(UserManager<User> userManager, IUserClaimsPrincipalFactory<User> userClaimsPrincipalFactory, IAuthorizationService authorizationService, RoleManager<IdentityRole> roleManager, IDateTimeProvider datetimeProvider, ITokenGennerator tokenGennerator, IBaseRepository<Patron> patronRepository, IEmailService emailService)
		{
			_userManager = userManager;
			_userClaimsPrincipalFactory = userClaimsPrincipalFactory;
			_authorizationService = authorizationService;
			_roleManager = roleManager;
			_datetimeProvider = datetimeProvider;
			this._tokenGennerator = tokenGennerator;
			_patronRepository = patronRepository;
			_emailService = emailService;
		}

		public async Task AddRefreshToken(string userName, string token)
		{
			var user = await _userManager.FindByNameAsync(userName);
			if(user != null)
			{
				user.RefreshToken = token;
				user.RefreshTokenExpiryTime = _datetimeProvider.Now.AddMinutes(10);
				await _userManager.UpdateAsync(user);
			}
		}

		public async Task<ErrorOr<bool>> AddRole(string roleName)
		{
			var role = new IdentityRole { Name = roleName };
			var roleResult = await _roleManager.CreateAsync(role);
			if(roleResult.Succeeded)
			{
				return true;
			}
			return roleResult.Errors.Select(error => Error.Conflict(error.Description)).ToList();
		}

		public async Task<UserInfo?> FindUserByNameAsync(string userName)
		{
			var user = await _userManager.FindByNameAsync(userName);
			var roles = await _userManager.GetRolesAsync(user);
			if (user != null)
				return new(user.UserName, roles.ToList());
			return null;
		}

		public async Task<UserInfo?> FindUserByPatronIdAsync(int patronId)
		{
			var user = await _userManager.Users.Where(user => user.PatronId == patronId).FirstOrDefaultAsync();

			if (user == null)
				return null;
			var role = await _userManager.GetRolesAsync(user);
			return new(user.UserName, role.ToList());
		}

		public async Task<ErrorOr<UserInfo>> Login(string userNameOrEmail, string password)
		{
			var user = await _userManager.FindByNameAsync(userNameOrEmail) ?? await _userManager.FindByEmailAsync(userNameOrEmail);
			if(user == null)
			{
				return Error.Unauthorized("User name or password is incorrect");
			}
			var passwordResult = await _userManager.CheckPasswordAsync(user, password);
			if(passwordResult == false)
			{
				return Error.Unauthorized("User name or password is incorrect");
			}
			var roles = await _userManager.GetRolesAsync(user);
			return new UserInfo(user.UserName, roles.ToList());
		}

		public async Task<ErrorOr<ListAccountDto>> ListAccounts(int page, int pageSize, 
			int searchPatronId, string seachPatronName, string searchEmail,
			string searchName)
		{
			IList<User> users = new List<User>();
			var totalRecords = 0;

			if (searchPatronId == 0 && searchEmail == "" && searchName == "" && seachPatronName == "")
			{
				users = await _userManager.GetUsersInRoleAsync(nameof(RoleEnum.User));
				users = users.Skip((page - 1) * pageSize).Take(pageSize).ToList();
				
				totalRecords = await _userManager.Users.CountAsync() - 2;
			}
			else
			{
				var query = users.AsQueryable();
				
				query = query.Where(u => u.UserName != "Admin" && u.UserName != "Librarian");
				
				if(searchPatronId != 0)
					query = query.Where(user => user.PatronId == searchPatronId);
				if(searchEmail != "")
					query = query.Where(user => user.Email.Contains(searchEmail));
				if(searchName != "")
					query = query.Where(user => user.UserName.Contains(searchName));
				if (seachPatronName != "")
				{
					var patronQuery = _patronRepository.GetQueryable();
					patronQuery.Where(p => p.Name.Contains(seachPatronName));
					
					var patrons = await patronQuery.ToListAsync();

					foreach (var patron in patrons)
					{
							query.Where(u => u.PatronId == patron.Id);
					}
				}
				totalRecords = query.Count();
				users = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();	
			}

			var listAccountRecords = new List<ListAccountRecord>();
			
			
			foreach (var user in users)
			{
				var patron = await _patronRepository.FindAsync((int)user.PatronId);
				
				var record = new ListAccountRecord(user.Id, patron.Id, patron.Name, user.UserName, user.Email);
				
				listAccountRecords.Add(record);
			}

			return new ListAccountDto(listAccountRecords, totalRecords);
		}

		public async Task<bool> ResetPassword(string userName)
		{
			var user = await _userManager.FindByNameAsync(userName);
			string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
			
			IdentityResult result = await _userManager.ResetPasswordAsync(user, resetToken, "Abc.123");

			if (result.Succeeded)
				return true;
			return false;
		}


		public async Task<ErrorOr<AuthResult>> Refresh(string userName, string refreshToken)
		{
			var user = await _userManager.FindByNameAsync(userName);
			if (user == null)
				return Error.Failure("Token is invalid");

			if(user.UserName == userName && user.RefreshTokenExpiryTime > _datetimeProvider.Now)
			{
				user.RefreshToken = _tokenGennerator.GenerateRefreshToken();
				user.RefreshTokenExpiryTime = _datetimeProvider.Now.AddMinutes(10);
				await _userManager.UpdateAsync(user);

				var roles = await _userManager.GetRolesAsync(user);
				return new AuthResult(_tokenGennerator.GenerateJwt(new UserInfo(user.UserName, roles.ToList())), user.RefreshToken);
			}

			return Error.Failure();
		}

		public async Task<ErrorOr<bool>> ChangePassword(string userId, string currentPassword, string newPassword)
		{
			var user = await _userManager.FindByIdAsync(userId);

			var passwordResult = await _userManager.CheckPasswordAsync(user, currentPassword);
			
			var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);

			if (result.Succeeded)
				return true;
			
			return result.Errors.Select(error => Error.Conflict(error.Description)).ToList();
		}

		public async Task DeleteAccountAsync(int patronId)
		{
			var account = await _userManager.Users.Where(user => user.PatronId == patronId).FirstOrDefaultAsync();

			if (account == null)
				return;
			
			await _userManager.DeleteAsync(account);
		}

		public async Task<ErrorOr<UserInfo>> SignInAsync(RegisterInfo info)
		{
			var user = new User { UserName = info.userName, Email = info.patron.Email , PatronId = info.patron.Id, PhoneNumber = info.patron.PhoneNumber };

			var userResult = await _userManager.CreateAsync(user, info.password);

			var roleResult = await _userManager.AddToRoleAsync(user, nameof(RoleEnum.User));

			if(userResult.Succeeded && roleResult.Succeeded)
			{
				var message = new Message(
					new List<MailboxAddress> { new(user.UserName, user.Email) },
					"Welcome to the Library",
					$"Welcome to the Library, we are glad to have you as our patron.\n" +
					$"Your account name is {user.UserName}.\n" +
					$"Your passwork is {info.password}"
				);
				await _emailService.SendEmailAsync(message);
				return new UserInfo(user.UserName, new List<string> { nameof(RoleEnum.User) });
			}
			var errors = userResult.Errors.Select(error => Error.Conflict(error.Description)).ToList().Concat(roleResult.Errors.Select(error => Error.Conflict(error.Description)).ToList());
			return errors.ToList();
		}
		
		
	}
}
