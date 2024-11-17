using LibraryManagement.Domain.LocationAggregate;
using MediatR;
using ErrorOr;

namespace LibraryManagement.Application.Locations.Create;

public record CreateLocationCommand(string Name) : IRequest<ErrorOr<bool>>;