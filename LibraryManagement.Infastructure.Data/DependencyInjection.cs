using LibraryManagement.Domain.BookAggregate;
using LibraryManagement.Domain.Common.Interface;
using LibraryManagement.Infastructure.Data.Data;
using LibraryManagement.Infastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Infastructure.Data
{
	public static class DependencyInjection 
	{
		public static IServiceCollection AddInfastructure(this IServiceCollection service, IConfiguration _config)
		{
			AddPersistence(service, _config);
			return service;
		}

		private static void AddPersistence(IServiceCollection service, IConfiguration _config)
		{
			service.AddDbContext<LibraryManagementContext>(options =>
			{
				options.UseSqlServer(_config.GetConnectionString("Default"));
			});

			service.AddScoped<IBaseRepository<Book>, BookRepository>();
		}
	}
}
