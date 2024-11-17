using LibraryManagement.Domain.Common.BaseModels;

namespace LibraryManagement.Domain.GenreAggregate.Events;

public record DeletedGenre(int GenreId, List<int> BookIds) : IDomainEvent;