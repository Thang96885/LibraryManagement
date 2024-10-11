using LibraryManagement.Domain.Common.BaseModels;

namespace LibraryManagement.Domain.BookReservationAggregate.ValueObjects
{
    public class ReservationBookCopyId : ValueObject
    {
		public string Value { get; private set; }
		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return Value;
		}

		private ReservationBookCopyId(string value)
		{
			Value = value;
		}

		public static ReservationBookCopyId Create(string value)
		{
			return new(value);
		}
	}
}