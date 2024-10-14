using ErrorOr;
using LibraryManagement.Application.Auth.Common;
using LibraryManagement.Application.Common.Interface;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Auth.Login
{
	public class LoginCommandHandler : IRequestHandler<LoginCommand, ErrorOr<AuthResult>>
	{
		private readonly IIdentityService _identityService;
		private readonly ITokenGennerator _tokenGennerator;

		public LoginCommandHandler(IIdentityService identityService, ITokenGennerator tokenGennerator)
		{
			_identityService = identityService;
			_tokenGennerator = tokenGennerator;
		}

		public async Task<ErrorOr<AuthResult>> Handle(LoginCommand request, CancellationToken cancellationToken)
		{
			var authResult = await _identityService.Login(request.userNameOrEmail, request.password);

			if (authResult.IsError)
				return authResult.Errors;

			var token = new AuthResult(_tokenGennerator.GenerateJwt(authResult.Value), _tokenGennerator.GenerateRefreshToken());
			await _identityService.AddRefreshToken(authResult.Value.UserName, token.refreshToken);

			return token; 
		}
	}
}
