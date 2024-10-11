using LibraryManagement.Domain.Common.BaseModels;
using LibraryManagement.Domain.GenreAggregate.ValueObjects;

namespace LibraryManagement.Domain.ReturnRecordAggregate.ValueObjects
{
    public class ReturnRecordBorrowRecordId : ValueObject
    {
		public Guid Value { get; private set; }
		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return Value;
		}

		private ReturnRecordBorrowRecordId(Guid value)
		{
			Value = value;
		}

		public static ReturnRecordBorrowRecordId Create(Guid value)
		{
			return new(value);
		}
	}
}