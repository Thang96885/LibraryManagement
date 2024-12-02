using ErrorOr;
using LibraryManagement.Domain.Common.Interface;
using LibraryManagement.Domain.YearPublicationAggregate;
using MediatR;

namespace LibraryManagement.Application.PublicationYears.List;

public class ListPublicationYearQueryHandler : IRequestHandler<ListPublicationYearQuery, ErrorOr<ListPublicationYearDto>>
{
    private readonly IBaseRepository<PublicationYear> _publicationYearRepository;

    public ListPublicationYearQueryHandler(IBaseRepository<PublicationYear> publicationYearRepository)
    {
        _publicationYearRepository = publicationYearRepository;
    }

    public async Task<ErrorOr<ListPublicationYearDto>> Handle(ListPublicationYearQuery request, CancellationToken cancellationToken)
    {
        var publicationYear = new List<PublicationYear>();
        var totalNumberOfPublicationYears = 0;

        if (request.SearchYear == 0)
        {
            publicationYear = await _publicationYearRepository.ListAsync(request.Page, request.PageSize);
            totalNumberOfPublicationYears = _publicationYearRepository.GetNumberOfEntities();
        }
        else
        {
            var publicationYearQuery = _publicationYearRepository.GetQueryable();
            
            publicationYearQuery = publicationYearQuery.Where(p => p.Year == request.SearchYear);
            
            totalNumberOfPublicationYears = publicationYearQuery.Count();
            publicationYear = publicationYearQuery.Skip((request.Page - 1) * request.PageSize).Take(request.PageSize).ToList();
        }

        return new ListPublicationYearDto(
            publicationYear.Select(p => 
                new ListPublicationYearRecord(p.Id, p.Year, p.BookIds.Count)).ToList(),
            totalNumberOfPublicationYears);

    }
}