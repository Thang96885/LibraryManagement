using LibraryManagement.Domain.Common.BaseModels;

namespace LibraryManagement.Domain.BookAggregate.Events;

public record CreatedBook(Book Book) : IDomainEvent;