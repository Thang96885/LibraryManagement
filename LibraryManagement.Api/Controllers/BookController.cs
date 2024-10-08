using LibraryManagement.Application.BookServices.Create;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[AllowAnonymous]
	public class BookController : ApiController
	{
		private readonly ISender _sender;

		public BookController(ISender sender)
		{
			_sender = sender;
		}

		// POST: api/Book
		[HttpPost]
		public async Task<IActionResult> Post([FromBody] CreateBookCommand command)
		{
			var result = await _sender.Send(command);

			if(result.IsError)
			{
				return Problem(result.Errors);
			}
			return Ok(result);
		}
	}
}
