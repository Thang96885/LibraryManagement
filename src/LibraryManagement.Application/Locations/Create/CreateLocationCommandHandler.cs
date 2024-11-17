using ErrorOr;
using LibraryManagement.Domain.Common.Interface;
using LibraryManagement.Domain.LocationAggregate;
using MediatR;

namespace LibraryManagement.Application.Locations.Create;

public class CreateLocationCommandHandler : IRequestHandler<CreateLocationCommand, ErrorOr<bool>>
{
    
    private readonly IBaseRepository<Location> _locationRepository;

    public CreateLocationCommandHandler(IBaseRepository<Location> locationRepository)
    {
        _locationRepository = locationRepository;
    }

    public async Task<ErrorOr<bool>> Handle(CreateLocationCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var location = Location.Create(request.Name);

            _locationRepository.Add(location);

            await _locationRepository.SaveChangeAsync();

            return true;
        }
        catch (Exception ex)
        {
            return Error.Failure(ex.Message);
        }
    }
}