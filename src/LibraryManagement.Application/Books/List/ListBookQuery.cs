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
		int numberOfBooks,
		List<BookDto> books);
	
	public record ListBookQuery(int page,
		int pageSize, int bookId = 0, string bookTitle = "", string authorName = "") : IRequest<ErrorOr<ListBookDto>>;
}
