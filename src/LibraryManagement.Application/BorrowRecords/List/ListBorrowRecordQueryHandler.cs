using MediatR;
using ErrorOr;
using LibraryManagement.Domain.BookAggregate;
using LibraryManagement.Domain.BorrowRecordAggregate;
using LibraryManagement.Domain.Common.Interface;
using LibraryManagement.Domain.PatronAggregate;

namespace LibraryManagement.Application.BorrowRecords.List;

public class ListBorrowRecordQueryHandler : IRequestHandler<ListBorrowRecordQuery, ErrorOr<List<ListBorrowRecordDto>>>
{
    private readonly IBaseRepository<BorrowRecord> _borrowRecordRepository;
    private readonly IBaseRepository<Book> _bookRepository;
    private readonly IBaseRepository<Patron> _patronRepository;

    public ListBorrowRecordQueryHandler(IBaseRepository<BorrowRecord> borrowRecordRepository, IBaseRepository<Book> bookRepository, IBaseRepository<Patron> patronRepository)
    {
        _borrowRecordRepository = borrowRecordRepository;
        _bookRepository = bookRepository;
        _patronRepository = patronRepository;
    }

    public async Task<ErrorOr<List<ListBorrowRecordDto>>> Handle(ListBorrowRecordQuery request, CancellationToken cancellationToken)
    {
        var borrowRecordList = await _borrowRecordRepository.ListAsync(request.Page, request.PageSize);

        var result = new List<ListBorrowRecordDto>();

        foreach (var borrowRecord in borrowRecordList)
        {
            var patron = await _patronRepository.FindAsync(borrowRecord.PatronId.Value)!;
            var bookInfoList = new List<(int BookId, String BookName, int NumberOfBorrowedBooks)>();
            foreach (var bookIdInfo in borrowRecord.BookIds)
            {
                var book = await _bookRepository.FindAsync(bookIdInfo.BookId)!;
                bookInfoList.Add(new (book.Id, book.Title, bookIdInfo.BookCopyIds.Count));
            }

            var bookBorrowRecorDto = MappingToResult(patron, bookInfoList, borrowRecord);
            result.Add(bookBorrowRecorDto);
        }

        return result;
    }

    private ListBorrowRecordDto MappingToResult(Patron patron, 
        List<(int BookId, String BookName, int NumberOfBorrowedBooks)> bookInfoList, 
        BorrowRecord borrowRecord)
    {
        return new ListBorrowRecordDto(
            borrowRecord.Id,
            borrowRecord.BorrowDate,
            borrowRecord.DueDate,
            borrowRecord.IsReturned,
            patron.Id,
            patron.Name,
            bookInfoList.Select(bookInfo =>
                    new ListBorrowRecordBookInfo(bookInfo.BookId, bookInfo.BookName, 
                        bookInfo.NumberOfBorrowedBooks))
                .ToList());
    }
}