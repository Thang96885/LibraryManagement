using LibraryManagement.Domain.Common.BaseModels;

namespace LibraryManagement.Domain.PatronAggregate.Events;

public record DeletedPatron(Patron Patron) : IDomainEvent;