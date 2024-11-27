using LibraryManagement.Domain.AuthorAggregate.ValueObjects;
using LibraryManagement.Domain.Common.BaseModels;

namespace LibraryManagement.Domain.AuthorAggregate;

public class Author : AggregateRoot
{
    private readonly List<AuthorBookId> _bookIds = new();
    
    public string Name { get; private set; }
    
    public List<AuthorBookId> BookIds => _bookIds;

    private Author(string name)
    {
        this.Name = name;
    }

    public static Author Create(string name)
    {
        return new(name);
    }

    public void AddBook(AuthorBookId bookId)
    {
        _bookIds.Add(bookId);
    }

    public void RemoveBook(AuthorBookId bookId)
    {
        _bookIds.Remove(bookId);
    }
}