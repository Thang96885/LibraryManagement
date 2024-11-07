using LibraryManagement.Domain.Common.BaseModels;
using LibraryManagement.Domain.GenreAggregate.ValueObjects;

namespace LibraryManagement.Domain.ReturnRecordAggregate.ValueObjects
{
    public class ReturnRecordBorrowRecordId : ValueObject
    {
		public int Value { get; private set; }
		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return Value;
		}

		private ReturnRecordBorrowRecordId(int value)
		{
			Value = value;
		}

		public static ReturnRecordBorrowRecordId Create(int value)
		{
			return new(value);
		}
	}
}