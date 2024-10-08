using LibraryManagement.Domain.Common.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Domain.BookAggregate.ValueObjects
{
	public class BookReturnRecordId : ValueObject
	{
		public Guid Value { get; private set; }
		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return Value;
		}
		public BookReturnRecordId(Guid value)
		{
			Value = value;
		}
	}
}
