using LibraryManagement.Domain.Common.BaseModels;

namespace LibraryManagement.Domain.LocationAggregate.Events;

public record DeletedLocation(int LocationId, List<int> BookIds) : IDomainEvent;