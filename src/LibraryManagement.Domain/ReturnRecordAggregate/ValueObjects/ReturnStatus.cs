using LibraryManagement.Domain.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Domain.ReturnRecordAggregate.ValueObjects
{
    public class ReturnStatus
	{
		public ReturnRecordBookCopyId BookId { get; set; }
		public BookPhysicalCondition Condition { get; set; }

		private ReturnStatus(ReturnRecordBookCopyId bookId, BookPhysicalCondition condition)
		{
			BookId = bookId;
			Condition = condition;
		}

		public ReturnStatus Create(string BookId, BookPhysicalCondition condition)
		{
			return new(ReturnRecordBookCopyId.Create(BookId), condition);
		}
	}
}
