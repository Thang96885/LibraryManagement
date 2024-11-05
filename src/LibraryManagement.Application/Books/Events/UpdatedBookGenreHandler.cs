using LibraryManagement.Domain.BookAggregate.Events;
using LibraryManagement.Domain.BookAggregate.ValueObjects;
using LibraryManagement.Domain.Common.Interface;
using LibraryManagement.Domain.GenreAggregate;
using LibraryManagement.Domain.GenreAggregate.ValueObjects;
using MediatR;

namespace LibraryManagement.Application.Books.Events;

public class UpdatedBookGenreHandler : INotificationHandler<UpdatedBookGenres>
{
    private readonly IBaseRepository<Genre> _genreRepository;

    public UpdatedBookGenreHandler(IBaseRepository<Genre> genreRepository)
    {
        _genreRepository = genreRepository;
    }

    public async Task Handle(UpdatedBookGenres notification, CancellationToken cancellationToken)
    {
        var addGenres = new List<Genre>();

        foreach (var genreId in notification.addedBookGenreIds)
        {
            var genre = await _genreRepository.FindAsync(genreId.Value);
            addGenres.Add(genre);
        }

        var removeGenres = new List<Genre>();

        foreach (var genreId in notification.removedGenreIds)
        {
            var genre =await _genreRepository.FindAsync(genreId.Value);
            removeGenres.Add(genre);
        }

        foreach (var genre in addGenres)
        {
             genre.AddBookId(GenreBookId.Create(notification.bookId));
        }

        foreach (var genre in removeGenres)
        {
            genre.RemoveBookId(GenreBookId.Create(notification.bookId));
        }
        
    }
}