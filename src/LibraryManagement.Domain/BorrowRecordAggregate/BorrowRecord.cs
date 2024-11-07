using LibraryManagement.Domain.BorrowRecordAggregate.ValueObjects;
using LibraryManagement.Domain.Common.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using LibraryManagement.Domain.BorrowRecordAggregate.Events;

namespace LibraryManagement.Domain.BorrowRecordAggregate
{
	public class BorrowRecord : AggregateRoot
	{
		private readonly List<BorrowRecordBookId> _bookIds = new();
		public DateTime BorrowDate { get; private set; }
		public DateTime DueDate { get; private set; }
		public bool IsReturned { get; private set; }
		public BorrowRecordPatronId PatronId { get; private set; }
		public BorrowRecordReturnRecordId? ReturnRecordId { get; private set; }
		public IReadOnlyList<BorrowRecordBookId> BookIds => _bookIds.AsReadOnly();

		private BorrowRecord(
		int patronId, DateTime borrowDate, DateTime dueDate, List<BorrowRecordBookId> bookIds = null)
		{
			PatronId = BorrowRecordPatronId.Create(patronId);
			BorrowDate = borrowDate;
			DueDate = dueDate;
			_bookIds = bookIds ?? new List<BorrowRecordBookId>();
		}

		public static BorrowRecord Create(int patronId, DateTime borrowDate, DateTime dueDate, List<BorrowRecordBookId> bookIds = null)
		{
			var borrowRecord = new BorrowRecord(patronId, borrowDate, dueDate, bookIds);

			var bookIdsInfo = new List<(int BookId, List<string> BookCopyIds)>();

			foreach (var bookId in bookIds)
			{
				bookIdsInfo.Add(new (bookId.BookId, bookId.BookCopyIds));
			}
			
			
			borrowRecord.AddDomainEvent(new CreatedBorrowRecord(borrowRecord.Id, borrowRecord.PatronId.Value, bookIdsInfo));
			return borrowRecord;
		}

		private BorrowRecord()
		{

		}
    }
}
