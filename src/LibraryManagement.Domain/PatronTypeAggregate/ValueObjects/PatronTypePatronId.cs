using LibraryManagement.Domain.Common.BaseModels;

namespace LibraryManagement.Domain.PatronTypeAggregate.ValueObjects;

public class PatronTypePatronId : ValueObject
{
    public int Value { get; private set; }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    private PatronTypePatronId(int value)
    {
        Value = value;
    }

    public static PatronTypePatronId Create(int value)
    {
        return new (value);
    }
}