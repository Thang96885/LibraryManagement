using System.Security.Principal;
using ErrorOr;
using LibraryManagement.Application.Common.Interface;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace LibraryManagement.Application.Auth.ChangePassword;

public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, ErrorOr<bool>>
{
    private readonly IIdentityService _identityService;

    public ChangePasswordCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<ErrorOr<bool>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var result = await _identityService.ChangePassword(request.UserId, request.CurrentPassword, request.NewPassword);

        return result;
    }
}