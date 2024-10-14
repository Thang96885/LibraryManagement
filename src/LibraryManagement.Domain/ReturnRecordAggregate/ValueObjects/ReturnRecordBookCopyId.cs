using LibraryManagement.Domain.Common.BaseModels;

namespace LibraryManagement.Domain.ReturnRecordAggregate.ValueObjects
{
    public class ReturnRecordBookCopyId : ValueObject
    {
		public string Value { get; private set; }
		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return Value;
		}

		private ReturnRecordBookCopyId(string value)
		{
			Value = value;
		}

		public static ReturnRecordBookCopyId Create(string value)
		{
			return new(value);
		}
	}
}