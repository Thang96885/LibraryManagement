using ErrorOr;
using MediatR;

namespace LibraryManagement.Application.Authors.Delete;

public record DeleteAuthorCommand(int Id) : IRequest<ErrorOr<bool>>;