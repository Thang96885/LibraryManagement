using LibraryManagement.Domain.BorrowRecordAggregate.ValueObjects;
using LibraryManagement.Domain.Common.BaseModels;

namespace LibraryManagement.Domain.BookReservationAggregate.ValueObjects
{
    public class BookReservationPatronId : ValueObject
    {
		public int Value { get; private set; }
		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return Value;
		}

		private BookReservationPatronId(int value)
		{
			Value = value;
		}

		public static BookReservationPatronId Create(int value)
		{
			return new(value);
		}
	}
}