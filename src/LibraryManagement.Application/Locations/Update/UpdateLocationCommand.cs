using ErrorOr;
using MediatR;

namespace LibraryManagement.Application.Locations.Update;

public record UpdateLocationCommand(int Id, string UpdateName)  : IRequest<ErrorOr<bool>>;