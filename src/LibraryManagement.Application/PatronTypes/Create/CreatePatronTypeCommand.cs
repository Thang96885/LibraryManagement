using ErrorOr;
using MediatR;

namespace LibraryManagement.Application.PatronTypes.Create;

public record CreatePatronTypeCommand(string name, int DiscountPercent) : IRequest<ErrorOr<bool>>;