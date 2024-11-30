using ErrorOr;
using MediatR;

namespace LibraryManagement.Application.Authors.Create;

public record CreateAuthorCommand(string Name) : IRequest<ErrorOr<bool>>;