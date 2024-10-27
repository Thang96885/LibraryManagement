using LibraryManagement.Application.Common.Interface;
using LibraryManagement.Domain.PatronAggregate.Events;
using MediatR;

namespace LibraryManagement.Application.Auth.Events;

public class DeletedPatronHandler : INotificationHandler<DeletedPatron>
{
    private readonly IIdentityService _identityService;

    public DeletedPatronHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task Handle(DeletedPatron notification, CancellationToken cancellationToken)
    {
        await _identityService.DeleteAccountAsync(notification.Patron.Id);
    }
}