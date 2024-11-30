using ErrorOr;
using MediatR;

namespace LibraryManagement.Application.PublicationYears.Delete;

public record DeletePublicationYearCommand(int Id) : IRequest<ErrorOr<bool>>;