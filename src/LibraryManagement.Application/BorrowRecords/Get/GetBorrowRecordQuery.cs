using MediatR;
using ErrorOr;
namespace LibraryManagement.Application.BorrowRecords.Get;

public record GetBorrowRecordDto(
    int Id,
    DateTime BorrowDate,
    DateTime DueDate,
    bool IsReturned,
    int PatronId,
    string PatronName,
    List<GetBorrowRecordBookInfo> BookInfoList);

public record GetBorrowRecordBookInfo(
    int BookId,
    string BookName,
    List<GetBorrowRecordBookCopyInfo> BookCopyBorrowInfoList);

public record GetBorrowRecordBookCopyInfo(
    string Id,
    string Condition);

public record GetBorrowRecordQuery(int Id) : IRequest<ErrorOr<GetBorrowRecordDto>>;