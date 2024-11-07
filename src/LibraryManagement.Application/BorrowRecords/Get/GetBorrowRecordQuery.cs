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
    int BookCopyBorrowCount,
    List<string> BookCopyBorrowId);

public record GetBorrowRecordQuery(int Id) : IRequest<ErrorOr<GetBorrowRecordDto>>;