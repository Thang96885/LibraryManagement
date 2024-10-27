using ErrorOr;
using LibraryManagement.Application.Books.Common;
using LibraryManagement.Domain.BookAggregate;
using LibraryManagement.Domain.BookAggregate.ValueObjects;
using LibraryManagement.Domain.Common.Interface;
using LibraryManagement.Domain.GenreAggregate;
using MapsterMapper;
using MediatR;

namespace LibraryManagement.Application.Books.UpdateBookGenre;


public class UpdateBookGenreCommandHandler : IRequestHandler<UpdateBookGenreCommand,ErrorOr<BookDto>>
{
    private readonly IBaseRepository<Book> _bookRepository;
    private readonly IBaseRepository<Genre> _genreRepository;
    private readonly IMapper _mapper;

    public UpdateBookGenreCommandHandler(IBaseRepository<Book> bookRepository, IBaseRepository<Genre> genreRepository, IMapper mapper)
    {
        _bookRepository = bookRepository;
        _genreRepository = genreRepository;
        _mapper = mapper;
    }

    public async Task<ErrorOr<BookDto>> Handle(UpdateBookGenreCommand request, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.FindAsync(request.BookId);
        
        if(book == null)
            return Error.NotFound("Book with given id does not exist");

        var addGenreIds = request.AddGenreIds.Select(id => new BookGenreId(id)).ToList();
        var removeGenreIds = request.RemoveGenreIds.Select(id => new BookGenreId(id)).ToList();
        
        book.UpdateGenre(addGenreIds, removeGenreIds);
        
        _bookRepository.Update(book);
        
        return _mapper.Map<BookDto>(book);
    }
}