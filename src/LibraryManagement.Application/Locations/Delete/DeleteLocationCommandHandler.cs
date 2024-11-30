using ErrorOr;
using LibraryManagement.Domain.Common.Interface;
using LibraryManagement.Domain.LocationAggregate;
using MediatR;

namespace LibraryManagement.Application.Locations.Delete;

public class DeleteLocationCommandHandler : IRequestHandler<DeleteLocationCommand, ErrorOr<bool>>
{
    private readonly IBaseRepository<Location> _locationRepository;

    public DeleteLocationCommandHandler(IBaseRepository<Location> locationRepository)
    {
        _locationRepository = locationRepository;
    }

    public async Task<ErrorOr<bool>> Handle(DeleteLocationCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var location = await _locationRepository.FindAsync(request.Id);

            if (location == null)
                return Error.NotFound("Location not found");

            location.Delete();

            _locationRepository.Delete(location);

            await _locationRepository.SaveChangeAsync();

            return true;
        }
        catch (ArgumentException e)
        {
            return Error.Conflict(e.Message);
        }
        
    }
}