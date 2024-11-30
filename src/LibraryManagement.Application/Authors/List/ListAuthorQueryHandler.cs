using ErrorOr;
using LibraryManagement.Domain.AuthorAggregate;
using LibraryManagement.Domain.Common.Interface;
using MediatR;

namespace LibraryManagement.Application.Authors.List;

public class ListAuthorQueryHandler : IRequestHandler<ListAuthorQuery, ErrorOr<ListAuthorDto>>
{
    private readonly IBaseRepository<Author> _authorRepository;

    public ListAuthorQueryHandler(IBaseRepository<Author> authorRepository)
    {
        _authorRepository = authorRepository;
    }

    public async Task<ErrorOr<ListAuthorDto>> Handle(ListAuthorQuery request, CancellationToken cancellationToken)
    {
        var authorList = new List<Author>();
        var totalNumberOfAuthor = 0;

        if (request.searchName == "")
        {
            totalNumberOfAuthor = _authorRepository.GetNumberOfEntities();
            authorList = await _authorRepository.ListAsync(request.page, request.pageSize);
        }
        else
        {
            var authorQuery = _authorRepository.GetQueryable();
            authorQuery = authorQuery.Where(a => a.Name.Contains(request.searchName));
            totalNumberOfAuthor = authorQuery.Count();
            authorList = authorQuery.ToList();
        }

        return new ListAuthorDto(
            authorList.Select(a => new ListAuthorRecord(
                a.Id, a.Name, a.BookIds.Count())).ToList(),
            totalNumberOfAuthor);
    }
}