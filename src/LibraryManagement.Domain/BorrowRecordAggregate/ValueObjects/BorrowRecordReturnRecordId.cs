using LibraryManagement.Domain.Common.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Domain.BorrowRecordAggregate.ValueObjects
{
	public class BorrowRecordReturnRecordId : ValueObject
	{
		public int Value { get; private set; }
		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return Value;
		}

		private BorrowRecordReturnRecordId(int value)
		{
			Value = value;
		}

		public static BorrowRecordReturnRecordId Create(int value)
		{
			return new(value);
		}
	}
}
