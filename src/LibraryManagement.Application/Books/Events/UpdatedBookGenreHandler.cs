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
        var addGenres = await _genreRepository.FindAsync((genre) => 
            notification.addedBookGenreIds.Contains(new BookGenreId(genre.Id))
        );
        
        var removeGenres = await _genreRepository.FindAsync((genre) => notification.removedGenreIds.Contains(new BookGenreId(genre.Id)));

        foreach (var genre in addGenres)
        {
             genre.AddBookId(GenreBookId.Create(notification.bookId));
        }

        foreach (var genre in removeGenres)
        {
            genre.RemoveBookId(GenreBookId.Create(notification.bookId));
        }

        await _genreRepository.SaveChangeAsync();
    }
}