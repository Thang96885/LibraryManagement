using LibraryManagement.Domain.Common.Interface;
using LibraryManagement.Domain.GenreAggregate;
using LibraryManagement.Infastructure.Data.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Infastructure.Data.Repositories
{
	public class GenreRepository : IBaseRepository<Genre>
	{
		private readonly LibraryManagementContext _context;

		public GenreRepository(LibraryManagementContext context)
		{
			_context = context;
		}


		public void Add(Genre entity)
		{
			_context.Add(entity);
		}

		public void Delete(Genre entity)
		{
			_context.Remove(entity);
		}

		public Genre? Find(Guid id)
		{
			return _context.Genres.Find(id);
		}

		public async Task<Genre>? FindAsync(Guid id)
		{
			return await _context.Genres.FindAsync(id);
		}

		public List<Genre> List()
		{
			return _context.Genres.ToList();
		}

        public List<Genre> List(int page, int pageSize)
        {
            return _context.Genres.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        public async Task<List<Genre>> ListAsync()
		{
			return await _context.Genres.ToListAsync();
		}

        public async Task<List<Genre>> ListAsync(int page, int pageSize)
        {
            return await _context.Genres.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public IEnumerable<Genre> Find(Expression<Func<Genre, bool>> predicate)
        {
	        var result = _context.Genres.Where(predicate).ToList();
	        return result;
        }

        public IEnumerable<Genre> Find(Expression<Func<Genre, bool>> predicate, int page, int pageSize)
        {
	        var result = _context.Genres.Where(predicate).Skip((page - 1) * pageSize).Take(pageSize).ToList();
	        return result;
        }

        public async Task<IEnumerable<Genre>> FindAsync(Expression<Func<Genre, bool>> predicate)
        {
	        var result = await _context.Genres.Where(predicate).ToListAsync();
	        return result;
        }

        public async Task<IEnumerable<Genre>> FindAsync(Expression<Func<Genre, bool>> predicate, int page, int pageSize)
        {
	        var result = await _context.Genres.Where(predicate).Skip((page - 1) * pageSize).ToListAsync();
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

		public void Update(Genre entity)
		{
			_context.Update(entity);
		}
	}
}
