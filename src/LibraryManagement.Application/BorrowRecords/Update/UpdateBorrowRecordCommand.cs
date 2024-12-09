using ErrorOr;
using MediatR;

namespace LibraryManagement.Application.BorrowRecords.Update;

public record UpdateBorrowRecordCommand(int BorrowRecordId, DateTime DueDate) : IRequest<ErrorOr<bool>>;