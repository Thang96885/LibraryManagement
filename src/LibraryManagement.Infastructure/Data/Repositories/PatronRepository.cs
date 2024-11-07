using LibraryManagement.Domain.Common.Interface;
using LibraryManagement.Domain.PatronAggregate;
using LibraryManagement.Infastructure.Data.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Infastructure.Data.Repositories
{
	public class PatronRepository : IBaseRepository<Patron>
	{
		private readonly LibraryManagementContext _context;

		public PatronRepository(LibraryManagementContext context)
		{
			_context = context;
		}
		public void Add(Patron entity)
		{
			_context.Patrons.Add(entity);
		}

		public void Delete(Patron entity)
		{
			_context.Patrons.Remove(entity);
		}

		public Patron? Find(int id)
		{
			return _context.Patrons.Find(id);
		}

		public async Task<Patron>? FindAsync(int id)
		{
			return await _context.Patrons.FindAsync(id);
		}

		public List<Patron> List()
		{
			return _context.Patrons.ToList();
		}

        public List<Patron> List(int page, int pageSize)
        {
            return _context.Patrons.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        public async Task<List<Patron>> ListAsync()
		{
			return await _context.Patrons.ToListAsync();
		}

        public async Task<List<Patron>> ListAsync(int page, int pageSize)
        {
            return await _context.Patrons.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public IEnumerable<Patron> Find(Expression<Func<Patron, bool>> predicate)
        {
	        var result = _context.Patrons.Where(predicate).ToList();
	        return result;
        }

        public IEnumerable<Patron> Find(Expression<Func<Patron, bool>> predicate, int page, int pageSize)
        {
	        var result = _context.Patrons.Where(predicate).Skip((page - 1) * pageSize).Take(pageSize).ToList();
	        return result;
        }

        public async Task<IEnumerable<Patron>> FindAsync(Expression<Func<Patron, bool>> predicate)
        {
	        var result = await _context.Patrons.Where(predicate).ToListAsync();
	        return result;
        }

        public async Task<IEnumerable<Patron>> FindAsync(Expression<Func<Patron, bool>> predicate, int page, int pageSize)
        {
	        var result = await _context.Patrons.Where(predicate).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
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

		public void Update(Patron entity)
		{
			_context.Patrons.Update(entity);
		}
	}
}
