using ErrorOr;
using MediatR;

namespace LibraryManagement.Application.Auth.ListAccount;

public record ListAccountRecord(
    string Id,
    int PatronId,
    string PatronName,
    string AccountName,
    string Email);

public record ListAccountDto(
    List<ListAccountRecord> Accounts,
    int TotalNumberOfAccounts);


public record ListAccountQuery(
    int Page, int PageSize, int SearchPatronId = 0, 
    string SeachPatronName = "", string SearchEmail = ""
    , string SearchName = "") : IRequest<ErrorOr<ListAccountDto>>;