using ErrorOr;
using LibraryManagement.Domain.AuthorAggregate;
using LibraryManagement.Domain.Common.Interface;
using MediatR;

namespace LibraryManagement.Application.Authors.Delete;

public class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand, ErrorOr<bool>>
{
    private readonly IBaseRepository<Author> _authorRepository;

    public DeleteAuthorCommandHandler(IBaseRepository<Author> authorRepository)
    {
        _authorRepository = authorRepository;
    }

    public async Task<ErrorOr<bool>> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
    {
        var author = await _authorRepository.FindAsync(request.Id);
        
        if(author == null)
            return Error.NotFound("Author not found");
        
        _authorRepository.Delete(author);

        await _authorRepository.SaveChangeAsync();

        return true;
    }
}