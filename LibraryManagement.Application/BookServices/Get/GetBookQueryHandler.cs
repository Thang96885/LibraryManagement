using ErrorOr;
using LibraryManagement.Application.BookServices.Common;
using LibraryManagement.Domain.BookAggregate;
using LibraryManagement.Domain.Common.Interface;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.BookServices.Get
{
	/*public class GetBookQueryHandler : IRequestHandler<GetBookQuery, ErrorOr<BookDto>>
	{
		private readonly IBaseRepository<Book> _bookReposiotry;

		public GetBookQueryHandler(IBaseRepository<Book> bookReposiotry)
		{
			_bookReposiotry = bookReposiotry;
		}

		public async Task<ErrorOr<BookDto>> Handle(GetBookQuery request, CancellationToken cancellationToken)
		{
			var book = await _bookReposiotry.FindAsync(Guid.Parse(request.Id));

			if (book == null)
				return Error.NotFound();
		}


	}*/
}
