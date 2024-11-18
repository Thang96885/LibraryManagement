using Azure.Core;
using ErrorOr;
using MediatR;

namespace LibraryManagement.Application.Locations.List;

public record ListLocationRecord(int Id, string Name, int NumberOfBookLocated);

public record ListLocationDto(
    List<ListLocationRecord> locations,
    int TotalNumberOfLocations);

public record ListLocationQuery(int Page, int PageSize, 
    int LocationId = 0, string SearchName = "") : IRequest<ErrorOr<ListLocationDto>>;
