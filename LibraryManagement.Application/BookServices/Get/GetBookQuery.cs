using ErrorOr;
using LibraryManagement.Application.BookServices.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.BookServices.Get
{
	public record GetBookQuery(string Id) : IRequest<ErrorOr<BookDto>>;
}
