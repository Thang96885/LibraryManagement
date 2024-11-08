﻿using ErrorOr;
using LibraryManagement.Application.Auth.Common;
using LibraryManagement.Domain.PatronAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Common.Interface
{
	public record RegisterInfo (Patron patron, string userName, string password);

	public interface IIdentityService
	{
		Task DeleteAccountAsync(Guid patronId);
		Task<ErrorOr<UserInfo>> SignInAsync(RegisterInfo info);
		Task AddRefreshToken(string userName, string token);
		Task<ErrorOr<bool>> AddRole(string roleName);
		Task<UserInfo?> FindUserByNameAsync(string userName);
		Task<UserInfo?> FindUserByPatronIdAsync(string patronId);
		Task<ErrorOr<AuthResult>> Refresh(string userName, string refreshToken);

		Task<ErrorOr<UserInfo>> Login(string userNameOrEmail, string password);
	}
}
