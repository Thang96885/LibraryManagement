using LibraryManagement.Domain.BookAggregate.ValueObjects;
using LibraryManagement.Domain.Common.BaseModels;

namespace LibraryManagement.Domain.BookAggregate.Events;

public record UpdatedBookGenres(Guid bookId, List<BookGenreId> addedBookGenreIds, List<BookGenreId> removedGenreIds) : IDomainEvent;