using ErrorOr;
using LibraryManagement.Application.Books.Common;
using MediatR;

namespace LibraryManagement.Application.Books.UpdateBookGenre;

public record UpdateBookGenreCommand(int BookId, List<int> AddGenreIds, List<int> RemoveGenreIds) : IRequest<ErrorOr<BookDto>>;