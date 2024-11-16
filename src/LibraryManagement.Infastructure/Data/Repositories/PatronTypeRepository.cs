using System.Linq.Expressions;
using LibraryManagement.Domain.Common.Interface;
using LibraryManagement.Domain.PatronTypeAggregate;
using LibraryManagement.Infastructure.Data.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infastructure.Data.Repositories;

public class PatronTypeRepository : IBaseRepository<PatronType>
{
    private readonly LibraryManagementContext _context;

    public PatronTypeRepository(LibraryManagementContext context)
    {
        _context = context;
    }

    public int GetNumberOfEntities()
    {
        return _context.PatronType.Count();
    }

    public PatronType? Find(int id)
    {
        return _context.PatronType.Find(id);
    }

    public async Task<PatronType>? FindAsync(int id)
    {
        return await _context.PatronType.FindAsync(id);
    }

    public void Add(PatronType entity)
    {
        _context.PatronType.Add(entity);
    }

    public void Update(PatronType entity)
    {
        _context.PatronType.Update(entity);
    }

    public void Delete(PatronType entity)
    {
        _context.PatronType.Remove(entity);
    }

    public List<PatronType> List()
    {
        return _context.PatronType.ToList();
    }

    public List<PatronType> List(int page, int pageSize)
    {
        return _context.PatronType.Skip((page - 1) * pageSize).Take(pageSize).ToList();
    }

    public async Task<List<PatronType>> ListAsync()
    {
        return await _context.PatronType.ToListAsync();
    }

    public async Task<List<PatronType>> ListAsync(int page, int pageSize)
    {
        return await _context.PatronType.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
    }

    public IQueryable<PatronType> GetQueryable()
    {
        return _context.PatronType;
    }

    public IEnumerable<PatronType> Find(Expression<Func<PatronType, bool>> predicate)
    {
        return _context.PatronType.Where(predicate);
    }

    public IEnumerable<PatronType> Find(Expression<Func<PatronType, bool>> predicate, int page, int pageSize)
    {
        return _context.PatronType.Where(predicate).Skip((page - 1) * pageSize).Take(pageSize);
    }

    public async Task<IEnumerable<PatronType>> FindAsync(Expression<Func<PatronType, bool>> predicate)
    {
        return await _context.PatronType.Where(predicate).ToListAsync();
    }

    public async Task<IEnumerable<PatronType>> FindAsync(Expression<Func<PatronType, bool>> predicate, int page, int pageSize)
    {
        return await _context.PatronType.Where(predicate).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
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