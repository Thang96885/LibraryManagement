using ErrorOr;
using MediatR;

namespace LibraryManagement.Application.Patrons.Delete;

public record DeletePatronDto(int Id, string Name);

public record DeletePatronCommand(int Id) : IRequest<ErrorOr<DeletePatronDto>>;