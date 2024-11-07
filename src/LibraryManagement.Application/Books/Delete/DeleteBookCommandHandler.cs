using ErrorOr;
using LibraryManagement.Domain.BookAggregate;
using LibraryManagement.Domain.Common.Interface;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Books.Delete
{
    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, ErrorOr<string>>
    {
        private readonly IBaseRepository<Book> _bookRepository;

        public DeleteBookCommandHandler(IBaseRepository<Book> bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<ErrorOr<string>> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.FindAsync(request.Id);
            if (book == null)
            {
                return Error.NotFound();
            }

            book.Delete();
            _bookRepository.Delete(book);
            await _bookRepository.SaveChangeAsync();
            return book.Id.ToString();
        }
    }
}
