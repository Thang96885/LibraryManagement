using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddApplication(this IServiceCollection service)
		{
			service.AddMediatR(configuration => configuration.RegisterServicesFromAssemblies(typeof(DependencyInjection).Assembly));

			service.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

			return service;
		}
	}
}
