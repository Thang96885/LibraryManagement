using LibraryManagement.Application.Common.Interface;
using LibraryManagement.Domain.BorrowRecordAggregate;
using LibraryManagement.Domain.BorrowRecordAggregate.ValueObjects;
using LibraryManagement.Domain.Common.Interface;
using LibraryManagement.Domain.PatronAggregate.Events;
using LibraryManagement.Domain.PatronTypeAggregate;
using LibraryManagement.Domain.ReturnRecordAggregate;
using MediatR;

namespace LibraryManagement.Application.Patrons.Delete;

public class DeletedPatronEventHandler : INotificationHandler<DeletedPatron>
{
    private readonly IBaseRepository<PatronType> _patronTypeRepository;
    private readonly IBaseRepository<BorrowRecord> _borrowRecordRepository;
    private readonly IBaseRepository<ReturnRecord> _returnRecordRepository;
    private readonly IIdentityService _identityService;

    public DeletedPatronEventHandler(IBaseRepository<ReturnRecord> returnRecordRepository, IBaseRepository<BorrowRecord> borrowRecordRepository, IBaseRepository<PatronType> patronTypeRepository, IIdentityService identityService)
    {
        _returnRecordRepository = returnRecordRepository;
        _borrowRecordRepository = borrowRecordRepository;
        _patronTypeRepository = patronTypeRepository;
        _identityService = identityService;
    }

    public async Task Handle(DeletedPatron notification, CancellationToken cancellationToken)
    {
        var patronType = await _patronTypeRepository.FindAsync(notification.Patron.TypeId.Value);
        
        patronType.RemovePatron(notification.Patron.Id);

        await _identityService.DeleteAccountAsync(notification.Patron.Id);
        await _patronTypeRepository.SaveChangeAsync();
    }
}