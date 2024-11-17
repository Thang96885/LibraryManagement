using LibraryManagement.Domain.BookAggregate;
using LibraryManagement.Domain.BookAggregate.ValueObjects;
using LibraryManagement.Domain.Common.Interface;
using LibraryManagement.Domain.GenreAggregate;
using LibraryManagement.Domain.GenreAggregate.Events;
using MediatR;

namespace LibraryManagement.Application.Genres.Events;

public class DeletedGenreEventHandler : INotificationHandler<DeletedGenre>
{
    private readonly IBaseRepository<Book> _bookRepository;

    public DeletedGenreEventHandler(IBaseRepository<Book> bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task Handle(DeletedGenre notification, CancellationToken cancellationToken)
    {
        foreach (var bookId in notification.BookIds)
        {
            var book = await _bookRepository.FindAsync(bookId);
            
            book.DeletedGenre(new BookGenreId(notification.GenreId));
            
            _bookRepository.Update(book);
        }
    }
}