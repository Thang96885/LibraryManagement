﻿using Bogus;
using LibraryManagement.Application.Books.Create;
using LibraryManagement.Domain.BookAggregate;
using LibraryManagement.Infastructure.Data.Data;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FetchDataController : ControllerBase
	{
		private readonly ISender _sender;

		public FetchDataController(ISender sender)
		{
			_sender = sender;
		}

		[HttpGet]
		public async Task<IActionResult> FetchBooks()
		{
			var faker = new Faker<CreateBookCommand>()
				.CustomInstantiator(f => new CreateBookCommand(
					f.Lorem.Sentence(),
					f.Name.FullName(),
					f.Company.CompanyName(),
					f.Date.Past(10).Year,
					f.Random.Number(100, 1000)
				));

			var books = faker.Generate(100);

			foreach(var book in books)
			{
				await _sender.Send(book);
			}

			return Ok(books);
		}
	}
}