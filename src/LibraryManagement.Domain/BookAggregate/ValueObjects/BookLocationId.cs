using LibraryManagement.Domain.Common.BaseModels;
using LibraryManagement.Domain.PatronAggregate.Events;

namespace LibraryManagement.Domain.BookAggregate.ValueObjects;

public class BookLocationId : ValueObject
{
    public int Value { get; private set; }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
    public BookLocationId(int value)
    {
        Value = value;
    }

    public static BookLocationId Create(int value)
    {
        return new BookLocationId(value);
    }
    
    private BookLocationId() { }
}