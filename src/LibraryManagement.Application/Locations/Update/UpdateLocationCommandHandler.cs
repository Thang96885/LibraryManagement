using ErrorOr;
using LibraryManagement.Domain.Common.Interface;
using LibraryManagement.Domain.LocationAggregate;
using MediatR;

namespace LibraryManagement.Application.Locations.Update;

public class UpdateLocationCommandHandler : IRequestHandler<UpdateLocationCommand, ErrorOr<bool>>
{
    private readonly IBaseRepository<Location> _locationRepository;

    public UpdateLocationCommandHandler(IBaseRepository<Location> locationRepository)
    {
        _locationRepository = locationRepository;
    }

    public async Task<ErrorOr<bool>> Handle(UpdateLocationCommand request, CancellationToken cancellationToken)
    {
        var location = await _locationRepository.FindAsync(request.Id);
        
        if(location == null)
            return Error.NotFound("Location with id: " + request.Id + " was not found");
        
        location.Update(request.UpdateName);
        
        _locationRepository.Update(location);

        await _locationRepository.SaveChangeAsync();

        return true;
    }
}