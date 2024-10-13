using LibraryManagement.Application.Books.Create;
using LibraryManagement.Application.Books.Delete;
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
		[HttpPost("add")]
		public async Task<IActionResult> Add([FromBody] CreateBookCommand command)
		{
			var result = await _sender.Send(command);

			if(result.IsError)
			{
				return Problem(result.Errors);
			}
			return Ok(result.Value);
		}

		/*[HttpPost("Get")]
		public async Task<IActionResult> Get([FromBody] GetBookQuery query)
		{
			var result = await _sender.Send(query);

			if (result.IsError)
			{
				return Problem(result.Errors);
			}
			return Ok(result.Value);
		}*/

		[HttpPost("Delete")]
		public async Task<IActionResult> Delete([FromBody] DeleteBookCommand command)
		{
			var result = await _sender.Send(command);

            if (result.IsError)
            {
				return Problem(result.Errors);
            }
			return Ok(result.Value);
        }
	}
}
