using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Auth.AddRole
{
	public record AddRoleCommand(string RoleName) : IRequest<ErrorOr<bool>>;
}
