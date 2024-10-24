using LibraryManagement.Application.Genres.Create;
using LibraryManagement.Application.Genres.List;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class GenreController : ApiController
	{
		private readonly ISender _sender;

		public GenreController(ISender sender)
		{
			_sender = sender;
		}

		[HttpPost("add")]
		[AllowAnonymous]
		public async Task<IActionResult> Create([FromBody] CreateGenreCommand request)
		{
			var result = await _sender.Send(request);

			if (result.IsError)
				return Problem(result.Errors);
			return Ok(result.Value);
		}

		[HttpGet("list")]
		[AllowAnonymous]
		public async Task<IActionResult> List([FromQuery] ListGenreQuery request)
		{
			var result = await _sender.Send(request);

			if (result.IsError)
				return Problem(result.Errors);
			return Ok(result.Value);
		}
	}
}
