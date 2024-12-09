using ErrorOr;
using LibraryManagement.Domain.Common.Interface;
using LibraryManagement.Domain.PatronTypeAggregate;
using MediatR;
using Org.BouncyCastle.Bcpg;

namespace LibraryManagement.Application.PatronTypes.List;

public class ListPatronTypeQueryHandler : IRequestHandler<ListPatronTypeQuery, ErrorOr<ListPatronTypeDto>>
{
    private readonly IBaseRepository<PatronType> _patronTypeRepository;

    public ListPatronTypeQueryHandler(IBaseRepository<PatronType> patronTypeRepository)
    {
        _patronTypeRepository = patronTypeRepository;
    }

    public async Task<ErrorOr<ListPatronTypeDto>> Handle(ListPatronTypeQuery request, CancellationToken cancellationToken)
    {
        var listPatronType = new List<PatronType>();
        
        
        if(request.SearchId == 0 && request.SearchName == "")
        {
            listPatronType = await _patronTypeRepository.ListAsync(request.Page, request.PageSize); 
        }
        else
        {
            var patronTypeQuery = _patronTypeRepository.GetQueryable();
            
            if(request.SearchId != 0)
                patronTypeQuery = patronTypeQuery.Where(p => p.Id == request.SearchId);
            if(request.SearchName != "")
                patronTypeQuery = patronTypeQuery.Where(p => p.Name.Contains(request.SearchName));

            listPatronType = patronTypeQuery.ToList();
        }

        var patronTypeCount = _patronTypeRepository.GetNumberOfEntities();

        return new ListPatronTypeDto(listPatronType.Select(p => new ListPatronTypeRecord(
            p.Id, p.Name, p.BookRentalFee, p.PatronIds.Count)).ToList(), patronTypeCount);

    }
}