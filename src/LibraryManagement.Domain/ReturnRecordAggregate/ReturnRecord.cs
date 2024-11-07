using LibraryManagement.Domain.Common.BaseModels;
using LibraryManagement.Domain.Common.Enums;
using LibraryManagement.Domain.ReturnRecordAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Domain.ReturnRecordAggregate
{
    public class ReturnRecord : AggregateRoot
	{
		private readonly List<ReturnStatus> _bookReturnStatus = new();
		public ReturnRecordBorrowRecordId BorrowRecordId { get; private set; }
		public ReturnRecordPatronId PatronId { get; private set; }
		public DateTime ReturnDate { get; private set; }
		public decimal? LateFee { get; private set; }
		public decimal TotalFee { get; private set; }
		public  IReadOnlyList<ReturnStatus> BookReturnStatus => _bookReturnStatus;

		private ReturnRecord(int borrowRecordId, int patronId, DateTime returnDate, decimal? lateFee, decimal totalFee)
		{
			BorrowRecordId = ReturnRecordBorrowRecordId.Create(borrowRecordId);
			PatronId = ReturnRecordPatronId.Create(patronId);
			ReturnDate = returnDate;
			LateFee = lateFee;
			TotalFee = totalFee;

		}

		private ReturnRecord()
		{

		}
    }
}
