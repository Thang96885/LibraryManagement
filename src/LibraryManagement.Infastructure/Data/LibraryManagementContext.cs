using LibraryManagement.Domain.BookAggregate;
using LibraryManagement.Domain.BookAggregate.Entities;
using LibraryManagement.Domain.BookReservationAggregate;
using LibraryManagement.Domain.BorrowRecordAggregate;
using LibraryManagement.Domain.GenreAggregate;
using LibraryManagement.Domain.PatronAggregate;
using LibraryManagement.Domain.ReturnRecordAggregate;
using LibraryManagement.Infastructure.Data.Identity.Models;
using LibraryManagement.Infastructure.Data.Interceptor;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Infastructure.Data.Data
{
	public class LibraryManagementContext : IdentityDbContext<User>
	{
		private readonly PublishDomainEventInterceptor _publishDomainEventInterceptor;


		public LibraryManagementContext(DbContextOptions<LibraryManagementContext> options, 
			PublishDomainEventInterceptor publishDomainEventInterceptor) : base(options)
		{
			_publishDomainEventInterceptor = publishDomainEventInterceptor;
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(LibraryManagementContext).Assembly);
			modelBuilder.Entity<User>().HasIndex(user => user.PatronId).IsUnique();
			base.OnModelCreating(modelBuilder);
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.AddInterceptors(_publishDomainEventInterceptor);
			base.OnConfiguring(optionsBuilder);
		}

		public DbSet<Book> Books { get; set; }
		public DbSet<Patron> Patrons { get; set; }
		public DbSet<BookReservation> Reservations { get; set; }
        public DbSet<BorrowRecord> BorrowRecords { get; set; }
        public DbSet<ReturnRecord> ReturnRecords { get; set; }
		public DbSet<Genre> Genres {  get; set; } 
    }
}
