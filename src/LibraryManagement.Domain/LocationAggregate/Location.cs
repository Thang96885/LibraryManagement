using System.Collections;
using LibraryManagement.Domain.Common.BaseModels;
using LibraryManagement.Domain.LocationAggregate.Events;
using LibraryManagement.Domain.LocationAggregate.ValueObjects;

namespace LibraryManagement.Domain.LocationAggregate;

public class Location : AggregateRoot
{
    private readonly List<LocationBookId> _locaitonBookIds = new();
    
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

    public void Update(string Name)
    {
        this.Name = Name;
    }

    public void Delete()
    {
        var deleteEvent = new DeletedLocation(this.Id, this._locaitonBookIds.Select(id => id.Value).ToList());
        
        AddDomainEvent(deleteEvent);
    }
    
    

    private Location()
    {
        
    }
}