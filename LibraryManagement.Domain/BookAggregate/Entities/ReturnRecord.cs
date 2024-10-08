using LibraryManagement.Domain.Common.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Domain.BookAggregate.Entities
{
	public class ReturnRecord : Entity
	{
        public DateTime ReturnDate { get; private set; }
		public Guid BookId { get; private set; }
		public Guid BorrowRecordId { get; private set; }

		public ReturnRecord(Guid bookId, Guid borrowRecordId)
		{
			BookId = bookId;
			BorrowRecordId = borrowRecordId;
		}
	}
}
