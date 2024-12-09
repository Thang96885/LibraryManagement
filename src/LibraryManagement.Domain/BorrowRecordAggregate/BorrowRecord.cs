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
		public decimal TotalRentalFee { get; private set; }
		public BorrowRecordPatronId PatronId { get; private set; }
		public BorrowRecordReturnRecordId? ReturnRecordId { get; private set; }
		public IReadOnlyList<BorrowRecordBookId> BookIds => _bookIds.AsReadOnly();

		private BorrowRecord(
		int patronId,
		 DateTime borrowDate,
		 DateTime dueDate,
		List<BorrowRecordBookId> bookIds = null,
		int RetalFeePerBook = 0)
		{
			PatronId = BorrowRecordPatronId.Create(patronId);
			BorrowDate = borrowDate;
			DueDate = dueDate;
			_bookIds = bookIds ?? new List<BorrowRecordBookId>();
			TotalRentalFee = (DueDate - BorrowDate).Days * RetalFeePerBook * _bookIds.Sum(x => x.BookCopyIds.Count);
		}

		public static BorrowRecord Create(int patronId,
			DateTime borrowDate,
			DateTime dueDate,
			List<BorrowRecordBookId> bookIds = null,
			int retalFee = 0)
		{
			var borrowRecord = new BorrowRecord(patronId, borrowDate,
				dueDate, bookIds, retalFee);

			var bookIdsInfo = new List<(int BookId, List<string> BookCopyIds)>();

			foreach (var bookId in bookIds)
			{
				bookIdsInfo.Add(new (bookId.BookId, bookId.BookCopyIds));
			}
			
			borrowRecord.AddDomainEvent(new CreatedBorrowRecord(borrowRecord));
			return borrowRecord;
		}
		
		public void Update(DateTime dueDate, decimal rentalCost)
		{
			DueDate = dueDate;
			TotalRentalFee = (DueDate - BorrowDate).Days * rentalCost * _bookIds.Sum(x => x.BookCopyIds.Count);
		} 

		public void Delete()
		{
			this.AddDomainEvent(new DeletedBorrowRecord(this));
		}

		private BorrowRecord()
		{

		}
    }
}
