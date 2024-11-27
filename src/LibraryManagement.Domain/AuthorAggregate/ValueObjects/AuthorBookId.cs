using LibraryManagement.Domain.Common.BaseModels;

namespace LibraryManagement.Domain.AuthorAggregate.ValueObjects;

public class AuthorBookId : ValueObject
{
    public int Value { get; private set; }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    private AuthorBookId(int value)
    {
        Value = value;
    }

    public static AuthorBookId Create(int value)
    {
        return new(value);
    }
}