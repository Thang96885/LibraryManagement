using LibraryManagement.Application;
using LibraryManagement.Infastructure.Data;
using LibraryManagement.Infastructure.Data.Identity.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LibraryManagement.Application.Auth.AddRole;
using LibraryManagement.Application.PatronTypes.Create;
using LibraryManagement.Infastructure;
using MediatR;

namespace LibraryManagement.Api
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddApplication().AddInfastructure(builder.Configuration);

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			builder.Services.AddCors(options =>
			{
				options.AddPolicy("CorsPolicy",
					builder => builder
						.WithOrigins("http://localhost:3000") // Replace with your frontend URL
						.AllowAnyHeader()
						.AllowAnyMethod());
			});

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.MapPost("/createInitialUsers", async (UserManager<User> userManager, ISender sender ) =>
			{
				await sender.Send(new AddRoleCommand("User"));
				await sender.Send(new AddRoleCommand("Admin"));
				await sender.Send(new AddRoleCommand("Librarian"));
				await sender.Send(new CreatePatronTypeCommand("User", 0));
				
				var adminUser = new User { UserName = "admin", Email = "admin@example.com", PatronId = null };
				var librarianUser = new User { UserName = "librarian", Email = "librarian@example.com", PatronId = null};

				var adminResult = await userManager.CreateAsync(adminUser, "Admin.123");
				var librarianResult = await userManager.CreateAsync(librarianUser, "Librarian.123");

				if (adminResult.Succeeded)
				{
					
					
					await userManager.AddToRoleAsync(adminUser, "Admin");
				}

				if (librarianResult.Succeeded)
				{
					await userManager.AddToRoleAsync(librarianUser, "Librarian");
				}

				return Results.Ok(new { Message = "Initial users created successfully" });
			});

			app.UseHttpsRedirection();

			app.UseCors("CorsPolicy");
			
			app.UseAuthentication();
			app.UseAuthorization();

			app.MapControllers();

			app.Run();
		}
	}
}
