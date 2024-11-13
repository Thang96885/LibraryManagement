using LibraryManagement.Domain.Common.BaseModels;

namespace LibraryManagement.Domain.PatronAggregate.ValueObjects;

public class PatronPatronTypeId : ValueObject
{
    public int Value { get; private set; }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    private PatronPatronTypeId(int value)
    {
        Value = value;
    }

    public static PatronPatronTypeId Create(int value)
    {
        return new (value);
    }
}