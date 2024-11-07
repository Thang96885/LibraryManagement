using LibraryManagement.Domain.Common.BaseModels;

namespace LibraryManagement.Domain.BorrowRecordAggregate.Events;

public record CreatedBorrowRecord(
    int BorrowRecordId, int PatronId,
    List<(int BookId, List<string> BookCopyIds)> BookInfo) : IDomainEvent;

    