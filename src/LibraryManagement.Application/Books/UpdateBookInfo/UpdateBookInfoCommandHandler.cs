using ErrorOr;
using LibraryManagement.Domain.BookAggregate;
using LibraryManagement.Domain.BookAggregate.ValueObjects;
using LibraryManagement.Domain.Common.Interface;
using MediatR;

namespace LibraryManagement.Application.Books.UpdateBookInfo;

public class UpdateBookInfoCommandHandler : IRequestHandler<UpdateBookInfoCommand, ErrorOr<UpdateBookInfoDto>>
{
    private readonly IBaseRepository<Book> _bookRepository;

    public UpdateBookInfoCommandHandler(IBaseRepository<Book> bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<ErrorOr<UpdateBookInfoDto>> Handle(UpdateBookInfoCommand request, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.FindAsync(request.BookId);
        
        if(book == null)
            return Error.NotFound("book with id = " + request.BookId + " not found");
        
        book.UpdateBookInfo(request.Title, request.PublisherName,
            request.PublicationYearId, request.PageCount, request.LocationId,
            request.RemoveAuthorIds?.Select(id => BookAuthorId.Create(id)).ToList(),
            request.AddAuthorIds?.Select(id => BookAuthorId.Create(id)).ToList());
        
       _bookRepository.Update(book);

       await _bookRepository.SaveChangeAsync();

       return new UpdateBookInfoDto(book.Id);
    }
}