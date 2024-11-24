using ErrorOr;
using MediatR;

namespace LibraryManagement.Application.Auth.ResetPassword;

public record ResetPasswordCommand(string AccountName) : IRequest<ErrorOr<bool>>;