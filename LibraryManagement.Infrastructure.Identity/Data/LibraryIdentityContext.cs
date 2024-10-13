using LibraryManagement.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Infrastructure.Identity.Data
{
	public class LibraryIdentityContext : IdentityDbContext<User, Role, Guid>
	{
		public LibraryIdentityContext(DbContextOptions<LibraryIdentityContext> options) : base(options)
		{

		}
	}
}
