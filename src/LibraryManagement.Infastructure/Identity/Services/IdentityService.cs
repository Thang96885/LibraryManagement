using ErrorOr;
using LibraryManagement.Application.Common.Interface;
using LibraryManagement.Domain.Common.Enums;
using LibraryManagement.Domain.Common.Interface;
using LibraryManagement.Domain.PatronAggregate;
using LibraryManagement.Infastructure.Data.Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Infastructure.Data.Identity.Services
{
	public class IdentityService : IIdentityService
	{
		private readonly UserManager<User> _userManager;
		private readonly IUserClaimsPrincipalFactory<User> _userClaimsPrincipalFactory;
		private readonly IAuthorizationService _authorizationService;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly IDateTimeProvider _datetimeProvider;

		public IdentityService(UserManager<User> userManager, IUserClaimsPrincipalFactory<User> userClaimsPrincipalFactory, IAuthorizationService authorizationService, RoleManager<IdentityRole> roleManager, IDateTimeProvider datetimeProvider)
		{
			_userManager = userManager;
			_userClaimsPrincipalFactory = userClaimsPrincipalFactory;
			_authorizationService = authorizationService;
			_roleManager = roleManager;
			_datetimeProvider = datetimeProvider;
		}

		public async Task AddRefreshToken(string userName, string token)
		{
			var user = await _userManager.FindByNameAsync(userName);
			if(user != null)
			{
				user.RefreshToken = token;
				user.RefreshTokenExpiryTime = _datetimeProvider.Now;
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

		public async Task<UserInfo?> FindUserByPatronIdAsync(string patronId)
		{
			var user = await _userManager.Users.Where(user => user.PatronId == Guid.Parse(patronId)).FirstOrDefaultAsync();

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

		public async Task<ErrorOr<UserInfo>> SignInAsync(RegisterInfo info)
		{
			var user = new User { UserName = info.userName, Email = info.patron.Email , PatronId = info.patron.Id, PhoneNumber = info.patron.PhoneNumber };

			var userResult = await _userManager.CreateAsync(user, info.password);

			var roleResult = await _userManager.AddToRoleAsync(user, nameof(RoleEnum.User));

			if(userResult.Succeeded && roleResult.Succeeded)
			{
				return new UserInfo(user.UserName, new List<string> { nameof(RoleEnum.User) });
			}
			var errors = userResult.Errors.Select(error => Error.Conflict(error.Description)).ToList().Concat(roleResult.Errors.Select(error => Error.Conflict(error.Description)).ToList());
			return errors.ToList();
		}
	}
}
