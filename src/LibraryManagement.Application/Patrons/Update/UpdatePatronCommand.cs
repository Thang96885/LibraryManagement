using ErrorOr;
using MediatR;

namespace LibraryManagement.Application.Patrons.Update;

public record UpdatePatronCommand(
    int Id, string Name = "", string PhoneNumber = "",
    string Email = "", string Address = "",
    int PatronTypeId = 0) : IRequest<ErrorOr<bool>>;