using FluentValidation;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;

namespace LibraryManagement.Application
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddApplication(this IServiceCollection service)
		{
			service.AddMediatR(configuration => configuration.RegisterServicesFromAssemblies(typeof(DependencyInjection).Assembly));
			
			service.AddFluentValidationAutoValidation();
			service.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
			AddMappingConfig(service);

			return service;
		}

		private static void AddMappingConfig(IServiceCollection service)
		{
			var config = TypeAdapterConfig.GlobalSettings;
			config.Scan(Assembly.GetExecutingAssembly());

			service.AddSingleton(config);

			service.AddScoped<IMapper, ServiceMapper>();
		}
	}
}
