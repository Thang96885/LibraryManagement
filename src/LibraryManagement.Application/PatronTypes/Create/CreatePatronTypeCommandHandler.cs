using ErrorOr;
using LibraryManagement.Domain.Common.Interface;
using LibraryManagement.Domain.PatronTypeAggregate;
using MediatR;

namespace LibraryManagement.Application.PatronTypes.Create;

public class CreatePatronTypeCommandHandler : IRequestHandler<CreatePatronTypeCommand, ErrorOr<bool>>
{
    private readonly IBaseRepository<PatronType> _patronTypeRepository;

    public CreatePatronTypeCommandHandler(IBaseRepository<PatronType> patronTypeRepository)
    {
        _patronTypeRepository = patronTypeRepository;
    }

    public async Task<ErrorOr<bool>> Handle(CreatePatronTypeCommand request, CancellationToken cancellationToken)
    {
        
        var patronType = PatronType.Create(request.name, request.DiscountPercent);
        
        _patronTypeRepository.Add(patronType);

        await _patronTypeRepository.SaveChangeAsync();

        return true;
    }
}