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
	public record ListBookQuery(int page, int numberBook) : IRequest<ErrorOr<List<BookDto>>>;
}
