using LibraryManagement.Domain.Common.Interface;
using LibraryManagement.Domain.PatronAggregate;
using LibraryManagement.Infastructure.Data.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

		public Patron? Find(Guid id)
		{
			return _context.Patrons.Find(id);
		}

		public async Task<Patron>? FindAsync(Guid id)
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
