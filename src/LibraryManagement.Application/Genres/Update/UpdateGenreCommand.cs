using ErrorOr;
using MediatR;

namespace LibraryManagement.Application.Genres.Update;

public record UpdateGenreCommand(int Id, string Name) : IRequest<ErrorOr<bool>>;