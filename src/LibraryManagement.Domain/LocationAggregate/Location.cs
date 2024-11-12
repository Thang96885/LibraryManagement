using System.Collections;
using LibraryManagement.Domain.Common.BaseModels;
using LibraryManagement.Domain.LocationAggregate.ValueObjects;

namespace LibraryManagement.Domain.LocationAggregate;

public class Location : AggregateRoot
{
    private readonly List<LocationBookId> _locaitonBookIds;
    
    public string Name { get; private set; }

    public List<LocationBookId> LocationBookIds => _locaitonBookIds;

    private Location(string name)
    {
        Name = name;
    }

    public static Location Create(string name)
    {
        return new(name);
    }

    public void AddLocationBookId(LocationBookId locationBookId)
    {
        _locaitonBookIds.Add(locationBookId);
    }

    private Location()
    {
        
    }
}