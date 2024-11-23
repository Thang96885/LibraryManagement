using MediatR;
using ErrorOr;
using LibraryManagement.Application.Common.Interface;

namespace LibraryManagement.Application.Auth.ListAccount;

public class ListAccountQueryHandler : IRequestHandler<ListAccountQuery, ErrorOr<ListAccountDto>>
{
    private readonly IIdentityService _identityService;

    public ListAccountQueryHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<ErrorOr<ListAccountDto>> Handle(ListAccountQuery request, CancellationToken cancellationToken)
    {
        return await _identityService.ListAccounts(request.Page, request.PageSize, request.SearchPatronId,
            request.SeachPatronName, request.SearchEmail, request.SearchName);
    }
}