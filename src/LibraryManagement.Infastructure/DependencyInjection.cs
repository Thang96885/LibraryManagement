﻿using System.Text;
using LibraryManagement.Application.Common.Interface;
using LibraryManagement.Application.Common.Services;
using LibraryManagement.Domain.BookAggregate;
using LibraryManagement.Domain.BookReservationAggregate;
using LibraryManagement.Domain.BorrowRecordAggregate;
using LibraryManagement.Domain.Common.Interface;
using LibraryManagement.Domain.Common.Interface.DomainServices;
using LibraryManagement.Domain.GenreAggregate;
using LibraryManagement.Domain.PatronAggregate;
using LibraryManagement.Domain.ReturnRecordAggregate;
using LibraryManagement.Infastructure.Data.Data;
using LibraryManagement.Infastructure.Data.Data.Repositories;
using LibraryManagement.Infastructure.Data.Identity.Models;
using LibraryManagement.Infastructure.Data.Identity.Services;
using LibraryManagement.Infastructure.Data.Interceptor;
using LibraryManagement.Infastructure.Data.Repositories;
using LibraryManagement.Infastructure.Identity.Services;
using LibraryManagement.Infastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace LibraryManagement.Infastructure
{
    public static class DependencyInjection 
	{
		public static IServiceCollection AddInfastructure(this IServiceCollection service, IConfiguration config)
		{
			service.AddSingleton<IDateTimeProvider, DateTimeProvider>();
			AddPersistence(service, config);
			AddIdentity(service);
			AddAuth(service, config);
			AddEmailService(service, config);
			AddDomainServices(service);

			return service;
		}

		private static void AddDomainServices(IServiceCollection service)
		{
			service.AddScoped<IGenreService, GenreService>();
		}

		private static void AddEmailService(IServiceCollection service, IConfiguration config)
		{
			var emailConfig = config.GetSection("EmailConfiguration").Get<EmailConfiguration>();
			service.AddSingleton(emailConfig);
			service.AddScoped<IEmailService, EmailService>();
		}

		private static void AddAuth(IServiceCollection service, IConfiguration config)
		{
			service.AddSingleton<ITokenGennerator, TokenGennerator>();
			service.AddScoped<IIdentityService, IdentityService>();

			service.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
				{
					ValidateAudience = true,
					ValidateIssuer = true,
					ValidateLifetime = true,
					ValidAudience = config.GetSection("Jwt:Audience").Value,
					ValidIssuer = config.GetSection("Jwt:Issuer").Value,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("Jwt:Key").Value))
				};
			});
		}

		private static void AddIdentity(IServiceCollection service)
		{
			service.AddIdentityCore<User>()
						.AddRoles<IdentityRole>()
						.AddEntityFrameworkStores<LibraryManagementContext>();
		}

		private static void AddPersistence(IServiceCollection service, IConfiguration config)
		{
			service.AddDbContext<LibraryManagementContext>(options =>
			{
				options.UseSqlServer(config.GetConnectionString("Default"));
			});

			service.AddScoped<IBaseRepository<Book>, BookRepository>();
			service.AddScoped<IBaseRepository<Patron>, PatronRepository>();
			service.AddScoped<IBaseRepository<BookReservation>, ReservationRepository>();
			service.AddScoped<IBaseRepository<BorrowRecord>, BorrowRecordRepository>();
			service.AddScoped<IBaseRepository<ReturnRecord>, ReturnRecordRepository>();
			service.AddScoped<IBaseRepository<Genre>, GenreRepository>();

			service.AddScoped<PublishDomainEventInterceptor>();
		}
	}
}
