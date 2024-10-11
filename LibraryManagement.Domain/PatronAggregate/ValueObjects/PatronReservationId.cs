using LibraryManagement.Domain.Common.BaseModels;
using LibraryManagement.Domain.GenreAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Domain.PatronAggregate.ValueObjects
{
	public class PatronReservationId : ValueObject
	{
		public Guid Value { get; private set; }
		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return Value;
		}

		private PatronReservationId(Guid value)
		{
			Value = value;
		}

		public static PatronReservationId Create(Guid value)
		{
			return new(value);
		}
	}
}
