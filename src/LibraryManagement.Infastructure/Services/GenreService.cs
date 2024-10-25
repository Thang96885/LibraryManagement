using LibraryManagement.Domain.Common.Interface;
using LibraryManagement.Domain.Common.Interface.DomainServices;
using LibraryManagement.Domain.GenreAggregate;

namespace LibraryManagement.Infastructure.Services;

public class GenreService : IGenreService
{
    private readonly IBaseRepository<Genre> _genreRepository;

    public GenreService(IBaseRepository<Genre> genreRepository)
    {
        _genreRepository = genreRepository;
    }

    public bool IsGenreNameUnique(string genreName)
    {
        var genre = _genreRepository.Find(x => x.Name == genreName);

        if (genre.Count() != 0)
            return false;
        return true;
    }
}