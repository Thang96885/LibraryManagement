using LibraryManagement.Domain.Common.BaseModels;

namespace LibraryManagement.Domain.ReturnRecordAggregate.ValueObjects
{
    public class ReturnRecordPatronId : ValueObject
    {
		public int Value { get; private set; }
		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return Value;
		}

		private ReturnRecordPatronId(int value)
		{
			Value = value;
		}

		public static ReturnRecordPatronId Create(int value)
		{
			return new(value);
		}
	}
}