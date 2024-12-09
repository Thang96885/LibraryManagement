using ErrorOr;
using LibraryManagement.Domain.Common.Interface;
using LibraryManagement.Domain.PatronTypeAggregate;
using MediatR;

namespace LibraryManagement.Application.PatronTypes.Update;

public class UpdateTypePatronCommandHandler : IRequestHandler<UpdateTypePatornCommand, ErrorOr<bool>>
{
    private readonly IBaseRepository<PatronType> _patronTypeRepository;

    public UpdateTypePatronCommandHandler(IBaseRepository<PatronType> patronTypeRepository)
    {
        _patronTypeRepository = patronTypeRepository;
    }

    public async Task<ErrorOr<bool>> Handle(UpdateTypePatornCommand request, CancellationToken cancellationToken)
    {
        var patronType = await _patronTypeRepository.FindAsync(request.Id);
        
        if(patronType == null)
            return Error.NotFound("Patron type doesn't exist");
        
        patronType.Update(request.NewName, request.NewBookRentalFee);
        
        _patronTypeRepository.Update(patronType);

        await _patronTypeRepository.SaveChangeAsync();

        return true;
    }
}