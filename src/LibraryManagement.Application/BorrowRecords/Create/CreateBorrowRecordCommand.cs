using ErrorOr;
using MediatR;

namespace LibraryManagement.Application.BorrowRecords;

public record CreateBorrowRecordDto(
    Guid Id,
    DateTime BorrowDate,
    DateTime DueDate,
    List<(string BookCopyId, string BookName)> BookCopiesInfo);

public record CreateBorrowRecordCommand(
    List<CreateBorrowRecordBookInfo> BorrowRecordBooksInfo,
    DateTime DueDate,
    Guid PatronId
) : IRequest<ErrorOr<CreateBorrowRecordDto>>;

public record CreateBorrowRecordBookInfo(Guid BookId, List<string> BookCopyIds);
