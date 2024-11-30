using ErrorOr;
using LibraryManagement.Domain.AuthorAggregate;
using LibraryManagement.Domain.Common.Interface;
using MediatR;

namespace LibraryManagement.Application.Authors.Create;

public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, ErrorOr<bool>>
{
    private readonly IBaseRepository<Domain.AuthorAggregate.Author> _authorRepository;

    public CreateAuthorCommandHandler(IBaseRepository<Domain.AuthorAggregate.Author> authorRepository)
    {
        _authorRepository = authorRepository;
    }

    public async Task<ErrorOr<bool>> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
    {
        var author = Author.Create(request.Name);
        
        _authorRepository.Add(author);

        await _authorRepository.SaveChangeAsync();

        return true;
    }
}