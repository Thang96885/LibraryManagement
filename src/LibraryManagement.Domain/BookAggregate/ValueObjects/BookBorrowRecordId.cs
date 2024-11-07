using LibraryManagement.Domain.Common.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Domain.BookAggregate.ValueObjects
{
	public class BookBorrowRecordId : ValueObject
	{
		public int Value { get; private set; }
		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return Value;
		}
		public BookBorrowRecordId(int value)
		{
			Value = value;
		}
	}
}
