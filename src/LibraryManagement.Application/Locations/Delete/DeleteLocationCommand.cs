using ErrorOr;
using MediatR;

namespace LibraryManagement.Application.Locations.Delete;

public record DeleteLocationCommand(int Id) : IRequest<ErrorOr<bool>>;