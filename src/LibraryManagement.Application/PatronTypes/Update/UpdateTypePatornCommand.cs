using MediatR;
using ErrorOr;

namespace LibraryManagement.Application.PatronTypes.Update;

public record UpdateTypePatornCommand(int Id, 
    string NewName, int NewBookRentalFee) : IRequest<ErrorOr<bool>>;