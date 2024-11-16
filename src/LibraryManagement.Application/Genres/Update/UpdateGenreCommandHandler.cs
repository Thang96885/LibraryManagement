using ErrorOr;
using LibraryManagement.Domain.Common.Interface;
using LibraryManagement.Domain.GenreAggregate;
using MediatR;

namespace LibraryManagement.Application.Genres.Update;

public class UpdateGenreCommandHandler : IRequestHandler<UpdateGenreCommand, ErrorOr<bool>>
{
    private readonly IBaseRepository<Genre> _genreRepository;
    
    public async Task<ErrorOr<bool>> Handle(UpdateGenreCommand request, CancellationToken cancellationToken)
    {
        var genre = await _genreRepository.FindAsync(request.Id);
        
        if(genre == null)
            return Error.NotFound("Genre with given id does not exist");
        
        genre.Update(request.Name);

        await _genreRepository.SaveChangeAsync();

        return true;
    }
}