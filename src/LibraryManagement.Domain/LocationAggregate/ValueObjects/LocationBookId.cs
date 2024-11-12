using LibraryManagement.Domain.Common.BaseModels;

namespace LibraryManagement.Domain.LocationAggregate.ValueObjects;

public class LocationBookId : ValueObject
{
    public int Value { get; private set; }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
    public LocationBookId(int value)
    {
        Value = value;
    }

    public static LocationBookId Create(int value)
    {
        return new(value);
    }
}