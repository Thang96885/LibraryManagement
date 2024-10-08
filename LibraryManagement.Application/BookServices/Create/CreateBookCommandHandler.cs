using ErrorOr;
using LibraryManagement.Domain.BookAggregate;
using LibraryManagement.Domain.Common.Interface;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.BookServices.Create
{
	public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, ErrorOr<string>>
	{
		private readonly IBaseRepository<Book> _bookRepository;

		public CreateBookCommandHandler(IBaseRepository<Book> bookRepository)
		{
			_bookRepository = bookRepository;
		}

		public async Task<ErrorOr<string>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
		{
			try
			{
				var book = Book.Create(request.Title, Guid.Parse(request.AuthorId),
				request.PublisherName, request.PublicationYear, request.PageCount,
				request.NumberOfCopy, request.NumberOfCopy);

				_bookRepository.Add(book);

				await _bookRepository.SaveChangeAsync();

				return book.Id.ToString();
			}
			catch(Exception ex)
			{
				return Error.Conflict();
			}
		}
	}
}
