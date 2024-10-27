using ErrorOr;
using MediatR;

namespace LibraryManagement.Application.Patrons.Delete;

public record DeletePatronDto(Guid Id, string Name);

public record DeletePatronCommand(Guid Id) : IRequest<ErrorOr<DeletePatronDto>>;