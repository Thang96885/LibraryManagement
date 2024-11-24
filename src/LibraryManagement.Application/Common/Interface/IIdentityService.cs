using ErrorOr;
using LibraryManagement.Application.Auth.Common;
using LibraryManagement.Domain.PatronAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagement.Application.Auth.ListAccount;
using Microsoft.AspNetCore.Identity;

namespace LibraryManagement.Application.Common.Interface
{
	public record RegisterInfo (Patron patron, string userName, string password);

	public interface IIdentityService
	{
		Task<ErrorOr<bool>> ChangePassword(string userId, string currentPassword, string newPassword);
		Task DeleteAccountAsync(int patronId);
		Task<ErrorOr<UserInfo>> SignInAsync(RegisterInfo info);
		Task AddRefreshToken(string userName, string token);
		Task<ErrorOr<bool>> AddRole(string roleName);
		Task<UserInfo?> FindUserByNameAsync(string userName);
		Task<UserInfo?> FindUserByPatronIdAsync(int patronId);
		Task<ErrorOr<AuthResult>> Refresh(string userName, string refreshToken);

		Task<ErrorOr<UserInfo>> Login(string userNameOrEmail, string password);

		Task<ErrorOr<ListAccountDto>> ListAccounts(int page, int pageSize, int searchPatronId,
			string seachPatronName, string searchEmail
			, string searchName);
		
		Task<bool> ResetPassword(string userName);
	}
}
