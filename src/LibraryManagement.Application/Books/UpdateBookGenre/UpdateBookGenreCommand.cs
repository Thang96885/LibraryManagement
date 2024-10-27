using ErrorOr;
using LibraryManagement.Application.Books.Common;
using MediatR;

namespace LibraryManagement.Application.Books.UpdateBookGenre;

public record UpdateBookGenreCommand(Guid BookId, List<Guid> AddGenreIds, List<Guid> RemoveGenreIds) : IRequest<ErrorOr<BookDto>>;