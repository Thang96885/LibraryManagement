using LibraryManagement.Domain.Common.BaseModels;

namespace LibraryManagement.Domain.BookAggregate.ValueObjects;

public class BookAuthorId : ValueObject
{
    public int Value { get; private set; }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    private BookAuthorId(int value)
    {
        Value = value;
    }

    public static BookAuthorId Create(int value)
    {
        return new(value);
    }
}