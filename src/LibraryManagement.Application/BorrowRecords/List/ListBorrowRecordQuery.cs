using ErrorOr;
using MediatR;

namespace LibraryManagement.Application.BorrowRecords.List;

public record ListBorrowRecordDto(
    Guid Id,
    DateTime BorrowDate,
    DateTime DueDate,
    bool IsReturned,
    Guid PatronId,
    string PatronName,
    List<ListBorrowRecordBookInfo> BookInfoList);

public record ListBorrowRecordBookInfo(
    Guid BookId,
    string BookName,
    int BookCopyBorrowCount);

public record ListBorrowRecordQuery(
    int Page = 1,
    int PageSize = 10) : IRequest<ErrorOr<List<ListBorrowRecordDto>>>;