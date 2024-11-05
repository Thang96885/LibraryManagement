using LibraryManagement.Domain.Common.BaseModels;

namespace LibraryManagement.Domain.BorrowRecordAggregate.Events;

public record CreatedBorrowRecord(
    Guid BorrowRecordId, Guid PatronId,
    List<(Guid BookId, List<string> BookCopyIds)> BookInfo) : IDomainEvent;

    