using ErrorOr;
using MediatR;

namespace LibraryManagement.Application.PublicationYears.Create;

public record CreatePublicationYearCommand(int year) : IRequest<ErrorOr<bool>>;