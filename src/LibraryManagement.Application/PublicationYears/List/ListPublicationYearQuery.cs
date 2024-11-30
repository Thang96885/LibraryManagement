using ErrorOr;
using MediatR;

namespace LibraryManagement.Application.PublicationYears.List;

public record ListPublicationYearRecord(
    int Id,
    int Year,
    int NumberOfBooks);

public record ListPublicationYearDto(
    List<ListPublicationYearRecord> Records,
    int TotalNumberOfYears);

public record ListPublicationYearQuery(int Page,
    int PageSize,
    int SearchYear) : IRequest<ErrorOr<ListPublicationYearDto>>;