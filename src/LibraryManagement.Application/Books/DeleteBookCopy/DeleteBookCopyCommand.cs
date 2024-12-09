using ErrorOr;
using MediatR;

namespace LibraryManagement.Application.Books.DeleteBookCopy;

public record DeleteBookCopyCommand(int BookId, string BookCopyId) : IRequest<ErrorOr<bool>>;