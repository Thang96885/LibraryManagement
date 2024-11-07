using MediatR;
using ErrorOr;
namespace LibraryManagement.Application.Auth.ChangePassword;

public record ChangePasswordCommand(
    string UserId, string CurrentPassword, string NewPassword) : IRequest<ErrorOr<bool>>;