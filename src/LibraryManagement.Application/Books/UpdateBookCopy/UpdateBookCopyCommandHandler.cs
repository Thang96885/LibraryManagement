using ErrorOr;
using LibraryManagement.Domain.BookAggregate;
using LibraryManagement.Domain.BookAggregate.ValueObjects;
using LibraryManagement.Domain.Common.Enums;
using LibraryManagement.Domain.Common.Interface;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace LibraryManagement.Application.Books.UpdateBookCopy;

public class UpdateBookCopyCommandHandler : IRequestHandler<UpdateBookCopyCommand,ErrorOr<bool>>
{
    private readonly IBaseRepository<Book> _bookRepository;

    public UpdateBookCopyCommandHandler(IBaseRepository<Book> bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<ErrorOr<bool>> Handle(UpdateBookCopyCommand request, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.FindAsync(request.BookId);
        
        if(book == null)
            return Error.NotFound("Book could not be found");

        var bookCopy = book.BookCopies.Where(b => b.Id == request.BookCopyIbns).FirstOrDefault();
        
        if(bookCopy == null)
            return Error.NotFound("Book copy could not be found");

        (BookStatus Status, BookPhysicalCondition Condition, decimal Price) updateValue = new();

        if (request.Status != "")
        {
            Enum.TryParse(request.Status, out updateValue.Status);
        }

        if (request.Condition != "")
        {
            Enum.TryParse(request.Condition, out updateValue.Condition);
        }

        updateValue.Price = request.Price;

        bookCopy.UpdateBookValue(updateValue.Status, updateValue.Condition, updateValue.Price);
        
        _bookRepository.Update(book);

        await _bookRepository.SaveChangeAsync();

        return true;
    }
}