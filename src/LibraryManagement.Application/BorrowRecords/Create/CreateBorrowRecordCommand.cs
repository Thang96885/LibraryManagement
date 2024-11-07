using ErrorOr;
using MediatR;

namespace LibraryManagement.Application.BorrowRecords;

public record CreateBorrowRecordDto(
    int Id,
    DateTime BorrowDate,
    DateTime DueDate,
    List<(string BookCopyId, string BookName)> BookCopiesInfo);

public record CreateBorrowRecordCommand(
    List<CreateBorrowRecordBookInfo> BorrowRecordBooksInfo,
    DateTime DueDate,
    int PatronId
) : IRequest<ErrorOr<CreateBorrowRecordDto>>;

public record CreateBorrowRecordBookInfo(int BookId, List<string> BookCopyIds);
