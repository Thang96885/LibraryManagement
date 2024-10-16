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

namespace LibraryManagement.Application.Auth.Refresh
{
	public class RefreshCommandHandler : IRequestHandler<RefreshCommand, ErrorOr<AuthResult>>
	{
		private readonly IIdentityService _identityService;
		private readonly ITokenGennerator _tokenGennerator;

		public RefreshCommandHandler(IIdentityService identityService, ITokenGennerator tokenGennerator)
		{
			_identityService = identityService;
			_tokenGennerator = tokenGennerator;
		}

		public async Task<ErrorOr<AuthResult>> Handle(RefreshCommand request, CancellationToken cancellationToken)
		{
			var userInfo = _tokenGennerator.DecodeJwt(request.jwtToken);
			if (userInfo == null)
				return Error.Failure("can not decode jwt token");

			var refreshResult = await _identityService.Refresh(userInfo.UserName, request.refreshToken);

			return refreshResult;
		}
	}
}
