using ErrorOr;
using MediatR;

namespace LibraryManagement.Application.Genres.Get;

public record GetGenreDto(int Id, string Name, int NumberOfBooks);

public record GetGenreQuery(int Id) : IRequest<ErrorOr<GetGenreDto>>;