using ErrorOr;
using MediatR;

namespace LibraryManagement.Application.BorrowRecords.List;

public record ListBorrowRecordDto(
    List<ListBorrowRecordBookRecord> Records,
    int TotalNumberOfRecords
);

public record ListBorrowRecordBookRecord(
    int Id,
    DateTime BorrowDate,
    DateTime DueDate,
    bool IsReturned,
    int PatronId,
    string PatronName,
    decimal RentalFee,
    List<ListBorrowRecordBookInfo> BookInfoList,
    int NumberBooksBorrowed
);

public record ListBorrowRecordBookInfo(
    int BookId,
    string BookName,
    List<string> BookCopyIds);

public record ListBorrowRecordQuery(
    int Page = 1,
    int PageSize = 10,
    bool NotReturned = false,
    int PatronId = 0) : IRequest<ErrorOr<ListBorrowRecordDto>>;