using LibraryManagement.Domain.Common.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Domain.PatronAggregate.ValueObjects
{
	public class PatronAddress : ValueObject
	{
		public string Street { get; private set; }
		public string City { get; private set; }
		public string State { get; private set; }
		public string ZipCode { get; private set; }
		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return Street;
			yield return City;
			yield return State;
			yield return ZipCode;

		}

		private PatronAddress(string street, string city, string state, string zipCode)
		{
			Street = street;
			City = city;
			State = state;
			ZipCode = zipCode;
		}
		public static PatronAddress Create(string street, string city, string state, string zipCode)
		{
			return new(street, city, state, zipCode);
		}
		public override string ToString()
		{
			return $"{Street}, {City}, {State}, {ZipCode}";
		}

		public static PatronAddress Parse(string value)
		{
			var parts = value.Split(',');
			if (parts.Length != 4)
				throw new FormatException("Invalid address format");
			return new(parts[0], parts[1], parts[2], parts[3]);
		}
	}
}
