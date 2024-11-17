using LibraryManagement.Domain.BookAggregate;
using LibraryManagement.Domain.Common.Interface;
using LibraryManagement.Domain.LocationAggregate.Events;
using MediatR;

namespace LibraryManagement.Application.Locations.Events;

public class DeletedLocationEventHandler : INotificationHandler<DeletedLocation>
{
    private readonly IBaseRepository<Book> _bookRepository;

    public DeletedLocationEventHandler(IBaseRepository<Book> bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task Handle(DeletedLocation notification, CancellationToken cancellationToken)
    {
        var books = new List<Book>();

        foreach (var bookId in notification.BookIds)
        {
            var book = await _bookRepository.FindAsync(bookId);
            
            book.DeletedLocation();
        }

        
    }
}