using ErrorOr;
using LibraryManagement.Domain.BookAggregate;
using LibraryManagement.Domain.Common.Interface;
using MediatR;

namespace LibraryManagement.Application.Books.DeleteBookCopy;

public class DeleteBookCopyCommandHandler : IRequestHandler<DeleteBookCopyCommand, ErrorOr<bool>>
{
    private readonly IBaseRepository<Book> _bookRepository;

    public DeleteBookCopyCommandHandler(IBaseRepository<Book> bookRepository)
    {
        _bookRepository = bookRepository;
    }


    public async Task<ErrorOr<bool>> Handle(DeleteBookCopyCommand request, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.FindAsync(request.BookId);
        
        if(book == null)
            return Error.NotFound("Book could not be found");
        
        book.DeleteBookCopy(request.BookCopyId);
        
        _bookRepository.Update(book);

        await _bookRepository.SaveChangeAsync();

        return true;
    }
}