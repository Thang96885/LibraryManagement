using LibraryManagement.Domain.Common.BaseModels;

namespace LibraryManagement.Domain.BookAggregate.Events;

public record UpdatedBook(int BookId, int PublicationYearId, int LocationId,
    List<int>? RemoveAuthorIds, List<int>? AddAuthorIds,
    List<int>? RemovdeGenreIds, List<int>? AddGenreIds) : IDomainEvent;