using MediatR;
using ErrorOr;
namespace LibraryManagement.Application.BorrowRecords.Get;

public record GetBorrowRecordDto(
    Guid Id,
    DateTime BorrowDate,
    DateTime DueDate,
    bool IsReturned,
    Guid PatronId,
    string PatronName,
    List<GetBorrowRecordBookInfo> BookInfoList);

public record GetBorrowRecordBookInfo(
    Guid BookId,
    string BookName,
    int BookCopyBorrowCount,
    List<string> BookCopyBorrowId);

public record GetBorrowRecordQuery(Guid Id) : IRequest<ErrorOr<GetBorrowRecordDto>>;