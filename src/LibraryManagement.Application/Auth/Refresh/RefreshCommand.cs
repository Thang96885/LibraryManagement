using ErrorOr;
using LibraryManagement.Application.Auth.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Auth.Refresh
{
	public record RefreshCommand(string jwtToken, string refreshToken) : IRequest<ErrorOr<AuthResult>>;
}
