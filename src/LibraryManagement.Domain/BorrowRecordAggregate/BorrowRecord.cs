using LibraryManagement.Domain.BorrowRecordAggregate.ValueObjects;
using LibraryManagement.Domain.Common.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Domain.BorrowRecordAggregate
{
	public class BorrowRecord : AggregateRoot
	{
		private readonly List<BorrowRecordBookCopyId> _bookIds = new();
		public DateTime BorrowDate { get; private set; }
		public DateTime DueDate { get; private set; }
		public bool IsReturned { get; private set; }
		public BorrowRecordPatronId PatronId { get; private set; }
		public BorrowRecordReturnRecordId? ReturnRecordId { get; private set; }
		public IReadOnlyList<BorrowRecordBookCopyId> BookIds => _bookIds.AsReadOnly();

		private BorrowRecord(Guid id, 
		Guid patronId, DateTime borrowDate, DateTime dueDate)
		{
			Id = id;
			PatronId = BorrowRecordPatronId.Create(patronId);
			BorrowDate = borrowDate;
			DueDate = dueDate;
		}

		public static BorrowRecord Create(Guid patronId, DateTime borrowDate, DateTime dueDate)
		{
			return new BorrowRecord(Guid.NewGuid(), patronId, borrowDate, dueDate);
		}

		private BorrowRecord()
		{

		}
    }
}
