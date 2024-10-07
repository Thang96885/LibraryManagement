using LibraryManagement.Domain.Common.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Domain.BookAggregate.ValueObjects
{
	public class BookGenreId : ValueObject
	{
		public Guid Value { get; private set; }
		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return Value;
		}

		private BookGenreId(Guid value)
		{
			Value = value;
		}

		public static BookGenreId Create(Guid Id)
		{
			return new(Id);
		}
	}
}
