using ErrorOr;
using LibraryManagement.Domain.Common.Interface;
using LibraryManagement.Domain.PatronAggregate;
using MediatR;

namespace LibraryManagement.Application.Patrons.Update;

public class UpdatePatronCommandHandler : IRequestHandler<UpdatePatronCommand, ErrorOr<bool>>
{
    private readonly IBaseRepository<Patron> _patronRepository;

    public UpdatePatronCommandHandler(IBaseRepository<Patron> patronRepository)
    {
        _patronRepository = patronRepository;
    }

    public async Task<ErrorOr<bool>> Handle(UpdatePatronCommand request, CancellationToken cancellationToken)
    {
        var patron = await _patronRepository.FindAsync(request.Id);
        
        if(patron == null)
            return Error.NotFound("Patron with id: " + request.Id + " does not exist");
        
        patron.Update(request.Name, request.PhoneNumber,
            request.Email, request.Address, request.PatronTypeId);
        
        _patronRepository.Update(patron);

        await _patronRepository.SaveChangeAsync();

        return true;
    }
}