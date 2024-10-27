using LibraryManagement.Application.Books.AddBookCopy;
using LibraryManagement.Application.Books.Create;
using LibraryManagement.Application.Books.Delete;
using LibraryManagement.Application.Books.Get;
using LibraryManagement.Application.Books.List;
using LibraryManagement.Application.Books.UpdateBookGenre;
using LibraryManagement.Domain.Common.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BookController : ApiController
	{
		private readonly ISender _sender;

		public BookController(ISender sender)
		{
			_sender = sender;
		}
		// POST: api/Book
		[HttpPost("add")]
		[Authorize(Roles = $"{nameof(RoleEnum.Admin)}, {nameof(RoleEnum.Librarian)}")]
		public async Task<IActionResult> Add([FromBody] CreateBookCommand command)
		{
			var result = await _sender.Send(command);

			if(result.IsError)
			{
				return Problem(result.Errors);
			}
			return Ok(result.Value);
		}
		[HttpGet("list")]
		[AllowAnonymous]
		public async Task<IActionResult> List([FromQuery] ListBookQuery request)
		{
			var result = await _sender.Send(request);
			if(result.IsError)
				return Problem(result.Errors);
			return Ok(result.Value);
		}

		[HttpGet("get/{id}")]
		public async Task<IActionResult> Get(string id)
		{
			var query = new GetBookQuery(id);
			var result = await _sender.Send(query);

			if (result.IsError)
			{
				return Problem(result.Errors);
			}
			return Ok(result.Value);
		}

		[HttpPost("add-book-copy")]
		[AllowAnonymous]
		public async Task<IActionResult> AddBookCopy(AddBookCopyCommand request)
		{
			var result = await _sender.Send(request);

			if (result.IsError)
				return Problem(result.Errors);
			return Ok(result.Value);
		}


		[HttpPost("Delete")]
		[Authorize(Roles = $"{nameof(RoleEnum.Admin)}, {nameof(RoleEnum.Librarian)}")]
		public async Task<IActionResult> Delete([FromBody] DeleteBookCommand command)
		{
			var result = await _sender.Send(command);

			if (result.IsError)
			{
				return Problem(result.Errors);
			}
			return Ok(result.Value);
		}

		[HttpPost("update-book-genre")]
		[AllowAnonymous]
		public async Task<IActionResult> UpdateBookGenre(UpdateBookGenreCommand command)
		{
			var result = await _sender.Send(command);
			if(result.IsError)
				return Problem(result.Errors);
			return Ok(result.Value);
		}
	}
}
