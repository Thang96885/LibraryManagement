using LibraryManagement.Domain.Common.Interface;
using LibraryManagement.Domain.PatronAggregate.Events;
using LibraryManagement.Domain.PatronTypeAggregate;
using MediatR;

namespace LibraryManagement.Application.Patrons.Update;

public class UpdatedPatronEventHandler : INotificationHandler<UpdatedPatronType>
{
    private readonly IBaseRepository<PatronType> _patronTypeRepository;

    public UpdatedPatronEventHandler(IBaseRepository<PatronType> patronTypeRepository)
    {
        _patronTypeRepository = patronTypeRepository;
    }

    public async Task Handle(UpdatedPatronType notification, CancellationToken cancellationToken)
    {
        try
        {
            var removePatronType = await _patronTypeRepository.FindAsync(notification.OldPatronTypeId);

            removePatronType.RemovePatron(notification.PatronId);

            var addPatronType = await _patronTypeRepository.FindAsync(notification.NewPatronTypeName);

            addPatronType.AddPatron(notification.PatronId);

            _patronTypeRepository.Update(addPatronType);
            _patronTypeRepository.Update(removePatronType);


            await _patronTypeRepository.SaveChangeAsync();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        
    }
}