using LibraryManagement.Domain.Common.BaseModels;
using LibraryManagement.Domain.PatronTypeAggregate.ValueObjects;

namespace LibraryManagement.Domain.PatronTypeAggregate;

public class PatronType : AggregateRoot
{
    private readonly IList<PatronTypePatronId> _patronIds = new List<PatronTypePatronId>();
    
    public string Name { get; private set; }
    public int DiscountPercent { get; private set; }
    public IList<PatronTypePatronId> PatronIds => _patronIds;

    private PatronType(string name, int discountPercent)
    {
        Name = name;
        DiscountPercent = discountPercent;
    }

    public static PatronType Create(string name, int discountPercent)
    {
        return new(name, discountPercent);
    }
    
}