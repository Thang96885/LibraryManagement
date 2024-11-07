using ErrorOr;
using MediatR;

namespace LibraryManagement.Application.BorrowRecords.List;

public record ListBorrowRecordDto(
    int Id,
    DateTime BorrowDate,
    DateTime DueDate,
    bool IsReturned,
    int PatronId,
    string PatronName,
    List<ListBorrowRecordBookInfo> BookInfoList);

public record ListBorrowRecordBookInfo(
    int BookId,
    string BookName,
    int BookCopyBorrowCount);

public record ListBorrowRecordQuery(
    int Page = 1,
    int PageSize = 10) : IRequest<ErrorOr<List<ListBorrowRecordDto>>>;