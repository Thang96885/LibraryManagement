using LibraryManagement.Domain.Common.Interface;
using LibraryManagement.Domain.ReturnRecordAggregate;
using LibraryManagement.Infastructure.Data.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Infastructure.Data.Repositories
{
	public class ReturnRecordRepository : IBaseRepository<ReturnRecord>
	{
		private readonly LibraryManagementContext _context;

		public ReturnRecordRepository(LibraryManagementContext context)
		{
			_context = context;
		}
		public void Add(ReturnRecord entity)
		{
			_context.ReturnRecords.Add(entity);
		}

		public void Delete(ReturnRecord entity)
		{
			_context.ReturnRecords.Remove(entity);
		}

		public ReturnRecord? Find(Guid id)
		{
			return _context.ReturnRecords.Find(id);
		}

		public async Task<ReturnRecord>? FindAsync(Guid id)
		{
			return await _context.ReturnRecords.FindAsync(id);
		}

		public List<ReturnRecord> List()
		{
			return _context.ReturnRecords.ToList();
		}

        public List<ReturnRecord> List(int page, int pageSize)
        {
            return _context.ReturnRecords.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        public async Task<List<ReturnRecord>> ListAsync()
		{
			return await _context.ReturnRecords.ToListAsync();
		}

        public async Task<List<ReturnRecord>> ListAsync(int page, int pageSize)
        {
            return await _context.ReturnRecords.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public int SaveChange()
		{
			return _context.SaveChanges();
		}

		public Task<int> SaveChangeAsync()
		{
			return _context.SaveChangesAsync();
		}

		public void Update(ReturnRecord entity)
		{
			_context.ReturnRecords.Update(entity);
		}
	}
}
