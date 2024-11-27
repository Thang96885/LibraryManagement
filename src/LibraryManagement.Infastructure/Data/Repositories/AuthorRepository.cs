using System.Linq.Expressions;
using LibraryManagement.Domain.AuthorAggregate;
using LibraryManagement.Domain.Common.Interface;
using LibraryManagement.Infastructure.Data.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infastructure.Data.Repositories;

public class AuthorRepository : IBaseRepository<Author>
{
    private readonly LibraryManagementContext _context;

    public AuthorRepository(LibraryManagementContext context)
    {
        _context = context;
    }

    public int GetNumberOfEntities()
    {
        return _context.Authors.Count();
    }

    public Author? Find(int id)
    {
        return _context.Authors.Find(id);
    }

    public async Task<Author>? FindAsync(int id)
    {
        return await _context.Authors.FindAsync(id);
    }

    public void Add(Author entity)
    {
        _context.Authors.Add(entity);
    }

    public void Update(Author entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
    }

    public void Delete(Author entity)
    {
        _context.Authors.Remove(entity);
    }

    public List<Author> List()
    {
        return _context.Authors.ToList();
    }

    public List<Author> List(int page, int pageSize)
    {
        return _context.Authors.Skip((page - 1) * pageSize).Take(pageSize).ToList();
    }

    public async Task<List<Author>> ListAsync()
    {
        return await _context.Authors.ToListAsync();
    }

    public async Task<List<Author>> ListAsync(int page, int pageSize)
    {
        return await _context.Authors.Skip((page - 1) * pageSize).Take(pageSize).AsNoTracking().ToListAsync();
    }

    public IQueryable<Author> GetQueryable()
    {
        return _context.Authors;
    }

    public IEnumerable<Author> Find(Expression<Func<Author, bool>> predicate)
    {
        return _context.Authors.Where(predicate);
    }

    public IEnumerable<Author> Find(Expression<Func<Author, bool>> predicate, int page, int pageSize)
    {
        return _context.Authors.Where(predicate).Skip((page - 1) * pageSize).Take(pageSize).ToList();
    }

    public async Task<IEnumerable<Author>> FindAsync(Expression<Func<Author, bool>> predicate)
    {
        return await _context.Authors.Where(predicate).ToListAsync();
    }

    public async Task<IEnumerable<Author>> FindAsync(Expression<Func<Author, bool>> predicate, int page, int pageSize)
    {
        return await _context.Authors.Where(predicate).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
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