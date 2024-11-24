using MediatR;
using ErrorOr;
using LibraryManagement.Application.Common.Interface;

namespace LibraryManagement.Application.Auth.ResetPassword;

public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, ErrorOr<bool>>
{
    private readonly IIdentityService _identityService;

    public ResetPasswordCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<ErrorOr<bool>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var result = await _identityService.ResetPassword(request.AccountName);

        if (result)
            return true;
        return Error.Failure("Failed to reset password");
    }
}