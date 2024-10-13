using ErrorOr;
using LibraryManagement.Application.Common.Interface;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Auth.AddRole
{
	public class AddRoleCommandHandler : IRequestHandler<AddRoleCommand, ErrorOr<bool>>
	{
		private readonly IIdentityService _identityService;

		public AddRoleCommandHandler(IIdentityService identityService)
		{
			_identityService = identityService;
		}

		public async Task<ErrorOr<bool>> Handle(AddRoleCommand request, CancellationToken cancellationToken)
		{
			var result = await _identityService.AddRole(request.RoleName);

			return result;
		}
	}
}
