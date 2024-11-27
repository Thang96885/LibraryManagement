using LibraryManagement.Domain.Common.BaseModels;

namespace LibraryManagement.Domain.BookAggregate.Events;

public record UpdatedBook(int BookId, int authorId, int publicationYearId) : IDomainEvent;