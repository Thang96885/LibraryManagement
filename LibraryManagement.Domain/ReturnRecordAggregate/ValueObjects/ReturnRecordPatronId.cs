using LibraryManagement.Domain.Common.BaseModels;

namespace LibraryManagement.Domain.ReturnRecordAggregate.ValueObjects
{
    public class ReturnRecordPatronId : ValueObject
    {
		public Guid Value { get; private set; }
		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return Value;
		}

		private ReturnRecordPatronId(Guid value)
		{
			Value = value;
		}

		public static ReturnRecordPatronId Create(Guid value)
		{
			return new(value);
		}
	}
}