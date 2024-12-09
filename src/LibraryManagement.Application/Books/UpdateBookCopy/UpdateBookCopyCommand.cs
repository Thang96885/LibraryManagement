using ErrorOr;
using MediatR;

namespace LibraryManagement.Application.Books.UpdateBookCopy;

public record UpdateBookCopyCommand(int BookId,
    string BookCopyIbns,
    string Status,
    string Condition,
    decimal Price) : IRequest<ErrorOr<bool>>;