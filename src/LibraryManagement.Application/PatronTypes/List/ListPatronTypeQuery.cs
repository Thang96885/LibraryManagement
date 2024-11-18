using MediatR;
using ErrorOr;

namespace LibraryManagement.Application.PatronTypes.List;

public record ListPatronTypeRecord(int Id, string Name, int DiscountPercent, int NumberOfPatrons);

public record ListPatronTypeDto(
    List<ListPatronTypeRecord> ListPatronTypeRecords,
    int totalNumberOfPatronTypes);

public record ListPatronTypeQuery(int Page, int PageSize,int searchId = 0, string SearchName = "") : IRequest<ErrorOr<ListPatronTypeDto>>;