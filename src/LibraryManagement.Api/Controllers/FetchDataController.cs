using Bogus;
using ErrorOr;
using LibraryManagement.Application.Books.AddBookCopy;
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

		[HttpGet("book")]
		public async Task<IActionResult> FetchBooks()
		{
			var faker = new Faker<CreateBookCommand>()
				.CustomInstantiator(f => new CreateBookCommand(
					f.Lorem.Sentence(),
					f.Name.FullName(),
					f.PickRandom<int>(new int[] {1, 2}),
					f.Date.Past(10).Year,
					f.PickRandom<List<int>>(new List<int> {1}, new List<int> {2}),
					"",
					f.Lorem.Sentence(),
					f.PickRandom<int>(new int[] {1, 2})
				));

			var books = faker.Generate(100);

			foreach(var book in books)
			{
				await _sender.Send(book);
			}

			return Ok(books);
		}

		[HttpGet("book-copy")]
		public async Task<IActionResult> FetchBookCopys([FromQuery] int bookId)
		{
			var ibnsFaker = new Faker<string>()
				.CustomInstantiator(f => f.Random.Replace("##########"));

			var fakerBookCopyCommand = new Faker<AddBookCopyCommand>()
				.CustomInstantiator(f => new AddBookCopyCommand
				(
					bookId,
					ibnsFaker.Generate(50),
					f.PickRandom<decimal>(new decimal[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 })
				));

			var bookCopys = fakerBookCopyCommand.Generate(1);
			
			return Ok(await _sender.Send(bookCopys[0]));
			
			
		}
	}
}
