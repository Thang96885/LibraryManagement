using LibraryManagement.Domain.Common.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Domain.PatronAggregate.ValueObjects
{
	public class PatronReturnRecordId : ValueObject
	{
		public int Value { get; private set; }
		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return Value;
		}

		private PatronReturnRecordId(int value)
		{
			Value = value;
		}

		public static PatronReturnRecordId Create(int value)
		{
			return new(value);
		}
	}
}
