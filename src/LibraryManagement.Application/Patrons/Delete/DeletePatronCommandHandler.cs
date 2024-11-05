using MediatR;
using ErrorOr;
using LibraryManagement.Domain.Common.Interface;
using LibraryManagement.Domain.PatronAggregate;
using MapsterMapper;

namespace LibraryManagement.Application.Patrons.Delete;

public class DeletePatronCommandHandler : IRequestHandler<DeletePatronCommand, ErrorOr<DeletePatronDto>>
{
    private readonly IBaseRepository<Patron> _patronRepository;
    private readonly IMapper _mapper;

    public DeletePatronCommandHandler(IBaseRepository<Patron> patronRepository, IMapper mapper)
    {
        _patronRepository = patronRepository;
        _mapper = mapper;
    }

    public async Task<ErrorOr<DeletePatronDto>> Handle(DeletePatronCommand request, CancellationToken cancellationToken)
    {
        var patron = await _patronRepository.FindAsync(request.Id);
        
        if(patron == null)
            return Error.NotFound("Patron with given id doesn't exist");
        
        patron.Delete();
        
        _patronRepository.Delete(patron);

        await _patronRepository.SaveChangeAsync();
        
        return _mapper.Map<DeletePatronDto>(patron);
    }
}