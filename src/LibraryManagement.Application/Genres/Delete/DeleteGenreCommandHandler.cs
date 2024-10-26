using ErrorOr;
using LibraryManagement.Domain.Common.Interface;
using LibraryManagement.Domain.GenreAggregate;
using MediatR;

namespace LibraryManagement.Application.Genres.Delete;

public class DeleteGenreCommandHandler : IRequestHandler<DeleteGenreCommand, ErrorOr<DeleteGenreDto>>
{
    private readonly IBaseRepository<Genre> _genreRepository;

    public DeleteGenreCommandHandler(IBaseRepository<Genre> genreRepository)
    {
        _genreRepository = genreRepository;
    }

    public async Task<ErrorOr<DeleteGenreDto>> Handle(DeleteGenreCommand request, CancellationToken cancellationToken)
    {
        var genre = await _genreRepository.FindAsync(request.Id);

        if (genre == null)
            return Error.NotFound("Genre with given id doesn't exist");
        
        _genreRepository.Delete(genre);

        await _genreRepository.SaveChangeAsync();

        return new DeleteGenreDto(request.Id);
    }
}