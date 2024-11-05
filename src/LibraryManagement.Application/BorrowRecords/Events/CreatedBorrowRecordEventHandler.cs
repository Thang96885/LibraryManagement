using LibraryManagement.Domain.BookAggregate;
using LibraryManagement.Domain.BorrowRecordAggregate.Events;
using LibraryManagement.Domain.Common.Interface;
using LibraryManagement.Domain.PatronAggregate;
using MediatR;

namespace LibraryManagement.Application.BorrowRecords.Events;

public class CreatedBorrowRecordEventHandler : INotificationHandler<CreatedBorrowRecord>
{
    private readonly IBaseRepository<Patron> _patronRepository;
    private readonly IBaseRepository<Book> _bookRepository;

    public CreatedBorrowRecordEventHandler(IBaseRepository<Patron> patronRepository, IBaseRepository<Book> bookRepository)
    {
        _patronRepository = patronRepository;
        _bookRepository = bookRepository;
    }

    public async Task Handle(CreatedBorrowRecord notification, CancellationToken cancellationToken)
    {
        try
        {
            var patron = _patronRepository.Find(notification.PatronId)!;
            patron.AddBorrowRecordId(notification.BorrowRecordId);
            _patronRepository.Update(patron);

            foreach (var bookInfo in notification.BookInfo)
            {
                var book = _bookRepository.Find(bookInfo.BookId)!;

                book.BorrowBook(bookInfo.BookCopyIds);

                _bookRepository.Update(book);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}