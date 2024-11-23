using LibraryManagement.Domain.Common.BaseModels;

namespace LibraryManagement.Domain.PatronAggregate.Events;

public record UpdatedPatronType(int PatronId, int OldPatronTypeId, int NewPatronTypeName) : IDomainEvent;