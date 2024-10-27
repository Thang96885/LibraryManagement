using LibraryManagement.Domain.BorrowRecordAggregate;
using LibraryManagement.Domain.Common.Interface;
using LibraryManagement.Infastructure.Data.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Infastructure.Data.Repositories
{
	public class BorrowRecordRepository : IBaseRepository<BorrowRecord>
	{	
		private readonly LibraryManagementContext _context;

		public BorrowRecordRepository(LibraryManagementContext context)
		{
			_context = context;
		}
		public void Add(BorrowRecord entity)
		{
			_context.BorrowRecords.Add(entity);
		}

		public void Delete(BorrowRecord entity)
		{
			_context.BorrowRecords.Remove(entity);
		}

		public BorrowRecord? Find(Guid id)
		{
			return _context.BorrowRecords.Find(id);
		}

		public async Task<BorrowRecord>? FindAsync(Guid id)
		{
			return await _context.BorrowRecords.FindAsync(id);
		}

		public List<BorrowRecord> List()
		{
			return _context.BorrowRecords.ToList();
		}

        public List<BorrowRecord> List(int page, int pageSize)
        {
            return _context.BorrowRecords.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        public async Task<List<BorrowRecord>> ListAsync()
		{
			return await _context.BorrowRecords.ToListAsync();
		}

        public async Task<List<BorrowRecord>> ListAsync(int page, int pageSize)
        {
            return await _context.BorrowRecords.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public IEnumerable<BorrowRecord> Find(Expression<Func<BorrowRecord, bool>> predicate)
        {
	        var result = _context.BorrowRecords.Where(predicate).ToList();
	        return result;
        }

        public IEnumerable<BorrowRecord> Find(Expression<Func<BorrowRecord, bool>> predicate, int page, int pageSize)
        {
	        var result = _context.BorrowRecords.Where(predicate).Skip((page - 1) * pageSize).Take(pageSize).ToList();
	        return result;
        }

        public async Task<IEnumerable<BorrowRecord>> FindAsync(Expression<Func<BorrowRecord, bool>> predicate)
        {
	        var result = await _context.BorrowRecords.Where(predicate).ToListAsync();
	        return result;
        }

        public async Task<IEnumerable<BorrowRecord>> FindAsync(Expression<Func<BorrowRecord, bool>> predicate, int page, int pageSize)
        {
	        var result = await _context.BorrowRecords.Where(predicate).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
	        return result;
        }

        public int SaveChange()
		{
			return _context.SaveChanges();
		}

		public async Task<int> SaveChangeAsync()
		{
			return await _context.SaveChangesAsync();
		}

		public void Update(BorrowRecord entity)
		{
			_context.BorrowRecords.Update(entity);
		}
	}
}
