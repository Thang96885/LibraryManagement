﻿using LibraryManagement.Domain.BookReservationAggregate;
using LibraryManagement.Domain.Common.Interface;
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
	public class ReservationRepository : IBaseRepository<BookReservation>
	{
		private readonly LibraryManagementContext _context;

		public ReservationRepository(LibraryManagementContext context)
		{
			_context = context;
		}
		public void Add(BookReservation entity)
		{
			_context.Reservations.Add(entity);
		}

		public void Delete(BookReservation entity)
		{
			_context.Reservations.Remove(entity);
		}

		public BookReservation? Find(Guid id)
		{
			return _context.Reservations.Find(id);
		}

		public async Task<BookReservation>? FindAsync(Guid id)
		{
			return await _context.Reservations.FindAsync(id);
		}

		public List<BookReservation> List()
		{
			return _context.Reservations.ToList();
		}

        public List<BookReservation> List(int page, int pageSize)
        {
            return _context.Reservations.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        public async Task<List<BookReservation>> ListAsync()
		{
			return await _context.Reservations.ToListAsync();
		}

        public async Task<List<BookReservation>> ListAsync(int page, int pageSize)
        {
            return await _context.Reservations.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }
        public IEnumerable<BookReservation> Find(Expression<Func<BookReservation, bool>> predicate)
        {
	        var result = _context.Reservations.Where(predicate).ToList();
	        return result;
        }

        public IEnumerable<BookReservation> Find(Expression<Func<BookReservation, bool>> predicate, int page, int pageSize)
        {
	        var result = _context.Reservations.Where(predicate).Skip((page - 1) * pageSize).Take(pageSize).ToList();
	        return result;
        }

        public async Task<IEnumerable<BookReservation>> FindAsync(Expression<Func<BookReservation, bool>> predicate)
        {
	        var result = await _context.Reservations.Where(predicate).ToListAsync();
	        return result;
        }

        public async Task<IEnumerable<BookReservation>> FindAsync(Expression<Func<BookReservation, bool>> predicate, int page, int pageSize)
        {
	        var result = await _context.Reservations.Where(predicate).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
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

		public void Update(BookReservation entity)
		{
			_context.Reservations.Update(entity);
		}
	}
}
