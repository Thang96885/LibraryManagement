using ErrorOr;
using LibraryManagement.Domain.BookAggregate;
using LibraryManagement.Domain.BookAggregate.Entities;
using LibraryManagement.Domain.Common.Interface;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Books.AddBookCopy
{
	public class AddBookCopyCommandHandler : IRequestHandler<AddBookCopyCommand, ErrorOr<List<string>>>
	{
		private readonly IBaseRepository<Book> _bookRepository;
		private readonly IDateTimeProvider _dateTimeProvider;

		public AddBookCopyCommandHandler(IBaseRepository<Book> bookRepository, IDateTimeProvider dateTimeProvider)
		{
			_bookRepository = bookRepository;
			_dateTimeProvider = dateTimeProvider;
		}

		public async Task<ErrorOr<List<string>>> Handle(AddBookCopyCommand request, CancellationToken cancellationToken)
		{
			try
			{
				var book = await _bookRepository.FindAsync(request.BookId);

				if (book == null)
					return Error.NotFound("Book id does not found");

				foreach (var IBNS in request.IBNSCodes)
				{
					var bookCopy = BookCopy.Create(IBNS, _dateTimeProvider.Now);
					book.AddBookCopy(bookCopy);
				}
				_bookRepository.Update(book);
				await _bookRepository.SaveChangeAsync();
				return book.BookCopies.Select(bc => bc.Id).ToList();

			}	
			catch(ArgumentException ex)
			{
				return Error.Validation(ex.Message);
			}
			catch (Exception ex)
			{
				return Error.Failure(ex.Message);
			} 
			
		}
	}
}
