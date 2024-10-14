using LibraryManagement.Domain.BorrowRecordAggregate.ValueObjects;
using LibraryManagement.Domain.Common.BaseModels;

namespace LibraryManagement.Domain.BookReservationAggregate.ValueObjects
{
    public class BookReservationPatronId : ValueObject
    {
		public Guid Value { get; private set; }
		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return Value;
		}

		private BookReservationPatronId(Guid value)
		{
			Value = value;
		}

		public static BookReservationPatronId Create(Guid value)
		{
			return new(value);
		}
	}
}