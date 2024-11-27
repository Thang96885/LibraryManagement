using ErrorOr;
using LibraryManagement.Application.Books.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Books.List
{
	public record ListBookDto(
		int NumberOfBooks,
		List<BookDto> Books);
	
	public record ListBookQuery(int Page,
		int PageSize, int BookId = 0, string BookTitle = "",
		int AuthorId = 0, int LocationId = 0, int YearPublicationId = 0,
		bool IsAvailable = false) : IRequest<ErrorOr<ListBookDto>>;
}
