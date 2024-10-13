using LibraryManagement.Infrastructure.Identity.Data;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Infrastructure.Identity
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddInfrastructureIdentity(this IServiceCollection service)
		{
			service.AddDbContext<LibraryIdentityContext>(options =>
			{
				options.UseSql
			})
		}
	}
}
