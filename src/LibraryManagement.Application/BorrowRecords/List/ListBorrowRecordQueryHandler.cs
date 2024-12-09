using MediatR;
using ErrorOr;
using LibraryManagement.Domain.BookAggregate;
using LibraryManagement.Domain.BorrowRecordAggregate;
using LibraryManagement.Domain.Common.Interface;
using LibraryManagement.Domain.PatronAggregate;
using LibraryManagement.Domain.BorrowRecordAggregate.ValueObjects;

namespace LibraryManagement.Application.BorrowRecords.List;

public class ListBorrowRecordQueryHandler : IRequestHandler<ListBorrowRecordQuery, ErrorOr<ListBorrowRecordDto>>
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

    public async Task<ErrorOr<ListBorrowRecordDto>> Handle(ListBorrowRecordQuery request, CancellationToken cancellationToken)
    {
        var borrowRecords = new List<BorrowRecord>();
        var totalNumberOfRecords = 0;

        if(request.PatronId == 0 && request.NotReturned == false)
        {
            borrowRecords = await _borrowRecordRepository.ListAsync(request.Page, request.PageSize);
            totalNumberOfRecords = _borrowRecordRepository.GetNumberOfEntities();
        }
        else
        {
            var query = _borrowRecordRepository.GetQueryable();

            if(request.PatronId != 0)
            {
                query = query.Where(b => b.PatronId == BorrowRecordPatronId.Create(request.PatronId));
            }
            if(request.NotReturned)
            {
                query = query.Where(b => b.IsReturned == false);
            }

            borrowRecords = query.ToList().Skip((request.Page - 1) * request.PageSize).Take(request.PageSize).ToList();
            totalNumberOfRecords = query.Count();
        }

        var borrowRecordDtos = new List<ListBorrowRecordBookRecord>();

        foreach(var record in borrowRecords)
        {
            var patron = await _patronRepository.FindAsync(record.PatronId.Value);

            var bookInfoList = new List<ListBorrowRecordBookInfo>();

            int numberOfBooksBorrowed = 0;

            foreach(var book in record.BookIds)
            {
                var bookEntity = await _bookRepository.FindAsync(book.BookId);

                bookInfoList.Add(new ListBorrowRecordBookInfo(book.BookId, bookEntity.Title, book.BookCopyIds));
                numberOfBooksBorrowed += book.BookCopyIds.Count;
            }

            var recordDto = new ListBorrowRecordBookRecord(
                record.Id, record.BorrowDate, record.DueDate,
                 record.IsReturned, record.PatronId.Value,
                  patron.Name, record.TotalRentalFee, bookInfoList,
                  numberOfBooksBorrowed);
            borrowRecordDtos.Add(recordDto);
        }
        return new ListBorrowRecordDto(borrowRecordDtos, totalNumberOfRecords);
       
    }
}