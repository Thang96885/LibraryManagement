using ErrorOr;
using LibraryManagement.Domain.Common.Interface;
using LibraryManagement.Domain.PatronTypeAggregate;
using MediatR;

namespace LibraryManagement.Application.PatronTypes.Delete;

public class DeletePatronTypeCommandHandler : IRequestHandler<DeletePatronTypeCommand, ErrorOr<bool>>
{
    private readonly IBaseRepository<PatronType> _patronTypeRepository;

    public DeletePatronTypeCommandHandler(IBaseRepository<PatronType> patronTypeRepository)
    {
        _patronTypeRepository = patronTypeRepository;
    }

    public async Task<ErrorOr<bool>> Handle(DeletePatronTypeCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var patronType = await _patronTypeRepository.FindAsync(request.Id);

            if (patronType == null)
                return Error.NotFound("Patron type not found");

            patronType.Delete();

            _patronTypeRepository.Delete(patronType);

            await _patronTypeRepository.SaveChangeAsync();

            return true;
        }
        catch (AggregateException ex)
        {
            return Error.Conflict(ex.Message);
        }
    }
}