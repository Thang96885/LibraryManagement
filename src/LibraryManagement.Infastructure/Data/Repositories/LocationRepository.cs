using System.Linq.Expressions;
using LibraryManagement.Domain.Common.Interface;
using LibraryManagement.Domain.LocationAggregate;
using LibraryManagement.Infastructure.Data.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infastructure.Data.Repositories;

public class LocationRepository : IBaseRepository<Location>
{
    private readonly LibraryManagementContext _context;
    
    public int GetNumberOfEntities()
    {
        return _context.Location.Count();
    }

    public Location? Find(int id)
    {
        return _context.Location.Find(id);
    }

    public async Task<Location>? FindAsync(int id)
    {
        return await _context.Location.FindAsync(id);
    }

    public void Add(Location entity)
    {
        _context.Location.Add(entity);
    }

    public void Update(Location entity)
    {
        _context.Location.Update(entity);
    }

    public void Delete(Location entity)
    {
        _context.Location.Remove(entity);
    }

    public List<Location> List()
    {
        return _context.Location.ToList();
    }

    public List<Location> List(int page, int pageSize)
    {
        return _context.Location.Skip((page - 1) * pageSize).Take(pageSize).ToList();
    }

    public async Task<List<Location>> ListAsync()
    {
        return await _context.Location.ToListAsync();
    }

    public async Task<List<Location>> ListAsync(int page, int pageSize)
    {
        return await _context.Location.Skip((page - 1) * pageSize).Take(pageSize).AsNoTracking().ToListAsync();
    }

    public IQueryable<Location> GetQueryable()
    {
        return _context.Location;
    }

    public IEnumerable<Location> Find(Expression<Func<Location, bool>> predicate)
    {
        return _context.Location.Where(predicate);
    }

    public IEnumerable<Location> Find(Expression<Func<Location, bool>> predicate, int page, int pageSize)
    {
        return _context.Location.Where(predicate).Skip((page - 1) * pageSize).Take(pageSize).ToList();
    }

    public async Task<IEnumerable<Location>> FindAsync(Expression<Func<Location, bool>> predicate)
    {
        return await _context.Location.Where(predicate).ToListAsync();
    }

    public async Task<IEnumerable<Location>> FindAsync(Expression<Func<Location, bool>> predicate, int page, int pageSize)
    {
        return await _context.Location.Where(predicate).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
    }

    public int SaveChange()
    {
        return _context.SaveChanges();
    }

    public Task<int> SaveChangeAsync()
    {
        return _context.SaveChangesAsync();
    }
}