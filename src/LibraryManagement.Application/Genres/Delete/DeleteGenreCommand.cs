using ErrorOr;
using MediatR;

namespace LibraryManagement.Application.Genres.Delete;

public record DeleteGenreDto(int Id);

public record DeleteGenreCommand(int Id) :IRequest<ErrorOr<DeleteGenreDto>>;