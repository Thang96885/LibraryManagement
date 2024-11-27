using System.Linq.Expressions;
using LibraryManagement.Domain.Common.Interface;
using LibraryManagement.Domain.YearPublicationAggregate;
using LibraryManagement.Infastructure.Data.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infastructure.Data.Repositories;

public class PublicationYearRepository : IBaseRepository<PublicationYear>
{
    private readonly LibraryManagementContext _context;

    public PublicationYearRepository(LibraryManagementContext context)
    {
        _context = context;
    }

    public int GetNumberOfEntities()
    {
        return _context.PublicationYears.Count();
    }

    public PublicationYear? Find(int id)
    {
        return _context.PublicationYears.Find(id);
    }

    public async Task<PublicationYear>? FindAsync(int id)
    {
        var publicationYear = await _context.PublicationYears.FindAsync(id);
        return publicationYear;
    }

    public void Add(PublicationYear entity)
    {
        _context.PublicationYears.Add(entity);
    }

    public void Update(PublicationYear entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
    }

    public void Delete(PublicationYear entity)
    {
        _context.PublicationYears.Remove(entity);
    }

    public List<PublicationYear> List()
    {
        return _context.PublicationYears.ToList();
    }

    public List<PublicationYear> List(int page, int pageSize)
    {
        return _context.PublicationYears.Skip((page - 1) * pageSize).Take(pageSize).ToList();
    }

    public async Task<List<PublicationYear>> ListAsync()
    {
        return await _context.PublicationYears.ToListAsync();
    }

    public async Task<List<PublicationYear>> ListAsync(int page, int pageSize)
    {
        return await _context.PublicationYears.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
    }

    public IQueryable<PublicationYear> GetQueryable()
    {
        return _context.PublicationYears;
    }

    public IEnumerable<PublicationYear> Find(Expression<Func<PublicationYear, bool>> predicate)
    {
        return _context.PublicationYears.Where(predicate);
    }

    public IEnumerable<PublicationYear> Find(Expression<Func<PublicationYear, bool>> predicate, int page, int pageSize)
    {
        return _context.PublicationYears.Where(predicate).Skip((page - 1) * pageSize).Take(pageSize).ToList();
    }

    public async Task<IEnumerable<PublicationYear>> FindAsync(Expression<Func<PublicationYear, bool>> predicate)
    {
        return await _context.PublicationYears.Where(predicate).ToListAsync();
    }

    public async Task<IEnumerable<PublicationYear>> FindAsync(Expression<Func<PublicationYear, bool>> predicate, int page, int pageSize)
    {
        return await _context.PublicationYears.Where(predicate).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
    }

    public int SaveChange()
    {
        return _context.SaveChanges();
    }

    public async Task<int> SaveChangeAsync()
    {
        return await _context.SaveChangesAsync();
    }
}