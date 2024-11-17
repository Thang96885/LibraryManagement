using ErrorOr;
using LibraryManagement.Domain.Common.Interface;
using LibraryManagement.Domain.LocationAggregate;
using MediatR;

namespace LibraryManagement.Application.Locations.List;

public class ListLocationQueryHandler : IRequestHandler<ListLocationQuery, ErrorOr<ListLocationDto>>
{
    private readonly IBaseRepository<Location> _locationRepository;

    public ListLocationQueryHandler(IBaseRepository<Location> locationRepository)
    {
        _locationRepository = locationRepository;
    }

    public async Task<ErrorOr<ListLocationDto>> Handle(ListLocationQuery request, CancellationToken cancellationToken)
    {
        List<Location> locations;

        if (request.LocationId == 0 && request.SearchName == "")
        {
            locations = await _locationRepository.ListAsync(request.Page, request.PageSize);
        }
        else
        {
            var locationQuery = _locationRepository.GetQueryable();
            
            if(request.LocationId != 0)
                locationQuery = locationQuery.Where(x => x.Id == request.LocationId);
            if(request.SearchName != "")
                locationQuery = locationQuery.Where(x => x.Name.Contains(request.SearchName));

            locations = locationQuery.ToList();
        }

        var totalNumberLocation = _locationRepository.GetNumberOfEntities();

        return new ListLocationDto(
            locations.Select(l => new ListLocationRecord(l.Id, l.Name, l.LocationBookIds.Count)).ToList(), totalNumberLocation);
    }
}