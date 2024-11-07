using LibraryManagement.Domain.Common.BaseModels;
using LibraryManagement.Domain.GenreAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Domain.PatronAggregate.ValueObjects
{
	public class PatronBorrowRecordId : ValueObject
	{
		public int Value { get; private set; }
		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return Value;
		}

		private PatronBorrowRecordId(int value)
		{
			Value = value;
		}

		public static PatronBorrowRecordId Create(int value)
		{
			return new (value);
		}
	}
}
