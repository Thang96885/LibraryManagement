using ErrorOr;
using LibraryManagement.Application.Auth.Common;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Auth.Register
{
    public record RegisterCommand(string PatronId, string userName, string password) : IRequest<ErrorOr<AuthResult>>;
}
