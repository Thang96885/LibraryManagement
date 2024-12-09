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
using ErrorOr;
using Microsoft.Data.SqlClient;
using System.Text.RegularExpressions;

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

        public int GetNumberOfEntities()
        {
            return _context.Books.Count(); 
        }

        public Book? Find(int id)
        {
            return _context.Books.Find(id);
        }

        public async Task<Book?> FindAsync(int id)
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

        public IQueryable<Book> GetQueryable()
        {
            return _context.Books.AsNoTracking().AsQueryable();
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

        public async Task<int> SaveChangeAsync()
        {
            try
            {
                var result = await  _context.SaveChangesAsync();
                return result;
            }
            catch (DbUpdateConcurrencyException e)
            {
                Console.WriteLine(e);
                throw e;
            }
            catch (DbUpdateException e)
            {
                Console.WriteLine(e);
                if (e.InnerException != null)
                {
                    var innerEx = e.InnerException as SqlException;
                    if (innerEx.Number == 2627)
                    {
                        string duplicateKey = ExtractDuplicateKey(innerEx.Message);
                        if (!string.IsNullOrEmpty(duplicateKey))
                        {
                            Console.WriteLine($"Duplicate key value: {duplicateKey}");
                        }
                        throw new ArgumentException($"Duplicate key value: {duplicateKey}");
                    }
                }
                throw e;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw e;
            }
        }

        private string ExtractDuplicateKey(string message)
        {
            var match = Regex.Match(message, @"The duplicate key value is \((.*?)\)");
            if (match.Success)
            {
                return match.Groups[1].Value;
            }
            return null;
        }

        public void Update(Book entity)
        {
            _context.Update(entity);
        }
    }
}
