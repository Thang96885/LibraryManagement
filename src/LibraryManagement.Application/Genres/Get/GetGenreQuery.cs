using ErrorOr;
using MediatR;

namespace LibraryManagement.Application.Genres.Get;

public record GetGenreDto(Guid Id, string Name, int NumberOfBooks);

public record GetGenreQuery(Guid Id) : IRequest<ErrorOr<GetGenreDto>>;