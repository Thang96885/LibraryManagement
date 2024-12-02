using ErrorOr;
using MediatR;

namespace LibraryManagement.Application.PublicationYears.Update;

public record UpdatePublicationYearCommand(int PublicationYearId, int Year) : IRequest<ErrorOr<bool>>;