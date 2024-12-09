using LibraryManagement.Domain.Common.BaseModels;

namespace LibraryManagement.Domain.BorrowRecordAggregate.Events;

public record DeletedBorrowRecord(BorrowRecord record) : IDomainEvent;