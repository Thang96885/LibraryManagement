using LibraryManagement.Domain.Common.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Domain.BookAggregate.ValueObjects
{
	public class BookId : AggregateRootId<Guid>
	{
		public override Guid Value { get; protected set ; }

		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return Value;
		}

		private BookId(Guid value)
		{
			Value = value;
		}

		public static BookId Create(Guid Id)
		{
			return new(Id);
		}

		public static BookId CreateUnique(Guid Id)
		{
			return new(Guid.NewGuid());
		}
	}
}
