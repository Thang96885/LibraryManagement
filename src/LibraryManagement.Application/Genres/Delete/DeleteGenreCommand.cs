using ErrorOr;
using MediatR;

namespace LibraryManagement.Application.Genres.Delete;

public record DeleteGenreDto(Guid Id);

public record DeleteGenreCommand(Guid Id) :IRequest<ErrorOr<DeleteGenreDto>>;