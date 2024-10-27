using LibraryManagement.Domain.BookAggregate;
using LibraryManagement.Domain.Common.Interface;
using LibraryManagement.Infastructure.Data.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Infastructure.Data.Data.Repositories
{
    public class BookRepository : IBaseRepository<Book>
    {
        private readonly LibraryManagementContext _context;

        public BookRepository(LibraryManagementContext context)
        {
            _context = context;
        }

        public void Add(Book entity)
        {
            _context.Add(entity);
        }

        public void Delete(Book entity)
        {
            _context.Remove(entity);
        }

        public Book? Find(Guid id)
        {
            return _context.Books.Find(id);
        }

        public async Task<Book?> FindAsync(Guid id)
        {
            return await _context.Books.FindAsync(id);
        }

        public List<Book> List()
        {
            return _context.Books.ToList();
        }

        public List<Book> List(int page, int pageSize)
        {
            return _context.Books.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        public async Task<List<Book>> ListAsync()
        {
            return await _context.Books.AsNoTracking().ToListAsync();
        }

        public async Task<List<Book>> ListAsync(int page, int pageSize)
        {
            return await _context.Books.AsNoTracking().Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public IEnumerable<Book> Find(Expression<Func<Book, bool>> predicate)
        {
            var result = _context.Books.Where(predicate).ToList();
            return result;
        }

        public IEnumerable<Book> Find(Expression<Func<Book, bool>> predicate, int page, int pageSize)
        {
            var result = _context.Books.Where(predicate).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return result;
        }

        public async Task<IEnumerable<Book>> FindAsync(Expression<Func<Book, bool>> predicate)
        {
            var result = await _context.Books.Where(predicate).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Book>> FindAsync(Expression<Func<Book, bool>> predicate, int page, int pageSize)
        {
            var result = await _context.Books.Where(predicate).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            return result;
        }

        public int SaveChange()
        {
            return _context.SaveChanges();
        }

        public Task<int> SaveChangeAsync()
        {
            return _context.SaveChangesAsync();
        }

        public void Update(Book entity)
        {
            _context.Update(entity);
        }
    }
}
