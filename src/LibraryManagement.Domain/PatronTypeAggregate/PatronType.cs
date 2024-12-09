using LibraryManagement.Domain.Common.BaseModels;
using LibraryManagement.Domain.PatronTypeAggregate.ValueObjects;

namespace LibraryManagement.Domain.PatronTypeAggregate;

public class PatronType : AggregateRoot
{
    private readonly IList<PatronTypePatronId> _patronIds = new List<PatronTypePatronId>();
    
    public string Name { get; private set; }
    public int BookRentalFee { get; private set; }
    public IList<PatronTypePatronId> PatronIds => _patronIds;

    private PatronType(string name, int bookRentalFee)
    {
        Name = name;
        BookRentalFee = bookRentalFee;
    }

    public static PatronType Create(string name, int bookRetalFee)
    {
        return new(name, bookRetalFee);
    }

    public void Update(string Name = "", int bookRentalFee = -1)
    {
        if(Name != "")
            this.Name = Name;
        
        if(BookRentalFee != -1)
            this.BookRentalFee = bookRentalFee;
    }

    public void Delete()
    {
        if (this.PatronIds.Count > 0)
            throw new AggregateException($"Patron type still have {PatronIds} patrons");
    }

    public void AddPatron(int PatronId)
    {
        _patronIds.Add(PatronTypePatronId.Create(PatronId));
    }

    public void RemovePatron(int PatronId)
    {
        _patronIds.Remove(PatronTypePatronId.Create(PatronId));
    }

}