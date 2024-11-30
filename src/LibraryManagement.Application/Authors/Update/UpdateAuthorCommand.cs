using ErrorOr;
using MediatR;

namespace LibraryManagement.Application.Authors.Update;
 
public record UpdateAuthorCommand(int Id, string Name) : IRequest<ErrorOr<bool>>;