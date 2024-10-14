using LibraryManagement.Domain.Common.BaseModels;
using LibraryManagement.Domain.GenreAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Domain.BorrowRecordAggregate.ValueObjects
{
	public class BorrowRecordBookCopyId : ValueObject
	{
		public string Value { get; private set; }
		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return Value;
		}

		private BorrowRecordBookCopyId(string value)
		{
			Value = value;
		}

		public static BorrowRecordBookCopyId Create(string value)
		{
			return new(value);
		}
	}
}
