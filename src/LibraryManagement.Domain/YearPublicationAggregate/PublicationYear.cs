using LibraryManagement.Domain.AuthorAggregate.ValueObjects;
using LibraryManagement.Domain.BookAggregate;
using LibraryManagement.Domain.Common.BaseModels;
using LibraryManagement.Domain.YearPublicationAggregate.ValueObjects;

namespace LibraryManagement.Domain.YearPublicationAggregate;

public class PublicationYear : AggregateRoot
{
    private readonly List<PublicationYearBookId> _bookIds = new();
    
    public int Year { get; set; }
    public List<PublicationYearBookId> BookIds => _bookIds;

    public PublicationYear(int year)
    {
        Year = year;
    }

    public void AddBookId(PublicationYearBookId bookId)
    {
        if(_bookIds.Contains(bookId) == false)
            _bookIds.Add(bookId);
    }

    public void RemoveBookId(PublicationYearBookId bookId)
    {
        if (_bookIds.Contains(bookId) == false)
            return;
        _bookIds.Remove(bookId);
    }

    public void Update(int year)
    {
        Year = year;
    }
}