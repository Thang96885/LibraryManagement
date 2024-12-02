using LibraryManagement.Domain.Common.BaseModels;

namespace LibraryManagement.Domain.BookAggregate.ValueObjects;

public class BookPublicationYearId : ValueObject
{
    public int Value { get; private set; }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
    public BookPublicationYearId(int value)
    {
        Value = value;
    }

    public static BookPublicationYearId Create(int value)
    {
        return new BookPublicationYearId(value);
    }
    
    private BookPublicationYearId() { }
}