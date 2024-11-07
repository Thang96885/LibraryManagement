using ErrorOr;
using LibraryManagement.Domain.BookAggregate;
using LibraryManagement.Domain.BorrowRecordAggregate;
using LibraryManagement.Domain.Common.Interface;
using LibraryManagement.Domain.PatronAggregate;
using MediatR;

namespace LibraryManagement.Application.BorrowRecords.Get;

public class GetBorrowRecordQueryHandler : IRequestHandler<GetBorrowRecordQuery, ErrorOr<GetBorrowRecordDto>>
{
    private readonly IBaseRepository<BorrowRecord> _borrowRecordRepository;
    private readonly IBaseRepository<Book> _bookRepository;
    private readonly IBaseRepository<Patron> _patronRepository;
    
    public GetBorrowRecordQueryHandler(IBaseRepository<BorrowRecord> borrowRecordRepository, IBaseRepository<Book> bookRepository, IBaseRepository<Patron> patronRepository)
    {
        _borrowRecordRepository = borrowRecordRepository;
        _bookRepository = bookRepository;
        _patronRepository = patronRepository;
    }

    public async Task<ErrorOr<GetBorrowRecordDto>> Handle(GetBorrowRecordQuery request, CancellationToken cancellationToken)
    {
        var borrowRecord = await _borrowRecordRepository.FindAsync(request.Id)!;
        
        if(borrowRecord == null)
            return Error.NotFound("Brrow record with id: " + request.Id + " was not found");
        
        var patron = await _patronRepository.FindAsync(borrowRecord.PatronId.Value)!;
        var bookInfoList = new List<(int BookId, String BookName, int NumberOfBorrowedBooks, List<string> BookCopyIds)>();
        
        foreach (var bookIdInfo in borrowRecord.BookIds)
        {
            var book = await _bookRepository.FindAsync(bookIdInfo.BookId)!;
            bookInfoList.Add(new (book.Id, book.Title, bookIdInfo.BookCopyIds.Count, bookIdInfo.BookCopyIds));
        }
        
        return MappingToResult(patron, bookInfoList, borrowRecord);
    }
    
    private GetBorrowRecordDto MappingToResult(Patron patron, 
        List<(int BookId, String BookName, int NumberOfBorrowedBooks, List<string> BookCopyIds)> bookInfoList, 
        BorrowRecord borrowRecord)
    {
        return new GetBorrowRecordDto(
            borrowRecord.Id,
            borrowRecord.BorrowDate,
            borrowRecord.DueDate,
            borrowRecord.IsReturned,
            patron.Id,
            patron.Name,
            bookInfoList.Select(bookInfo =>
                    new GetBorrowRecordBookInfo(bookInfo.BookId, bookInfo.BookName, 
                        bookInfo.NumberOfBorrowedBooks, bookInfo.BookCopyIds))
                .ToList());
    }
}