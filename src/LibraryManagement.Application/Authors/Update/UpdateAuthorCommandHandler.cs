using ErrorOr;
using LibraryManagement.Domain.AuthorAggregate;
using LibraryManagement.Domain.Common.Interface;
using MediatR;

namespace LibraryManagement.Application.Authors.Update;

public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand, ErrorOr<bool>>
{
    private readonly IBaseRepository<Author> _authorRepository;

    public UpdateAuthorCommandHandler(IBaseRepository<Author> authorRepository)
    {
        _authorRepository = authorRepository;
    }

    public async Task<ErrorOr<bool>> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
    {
        var author = await _authorRepository.FindAsync(request.Id);
        
        if(author == null)
            return Error.NotFound("Author not found");
        
        author.Update(request.Name);
        
        _authorRepository.Update(author);

        await _authorRepository.SaveChangeAsync();

        return true;


    }
}