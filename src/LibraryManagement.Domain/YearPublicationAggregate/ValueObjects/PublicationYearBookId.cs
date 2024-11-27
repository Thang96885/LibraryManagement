using LibraryManagement.Domain.Common.BaseModels;

namespace LibraryManagement.Domain.YearPublicationAggregate.ValueObjects;

public class PublicationYearBookId : ValueObject
{
    public int Value { get; private set; }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    private PublicationYearBookId(int value)
    {
        Value = value;
    }

    public static PublicationYearBookId Create(int value)
    {
        return new(value);
    }
}