using LibraryManagement.Domain.BookAggregate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Infastructure.Data.Data
{
	public class LibraryManagementContext : DbContext
	{
		public LibraryManagementContext(DbContextOptions<LibraryManagementContext> options) : base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(LibraryManagementContext).Assembly);
			base.OnModelCreating(modelBuilder);
		}

		public DbSet<Book> Books { get; set; }
	}
}
