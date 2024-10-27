using LibraryManagement.Application.Patrons.Create;
using LibraryManagement.Application.Patrons.Get;
using LibraryManagement.Application.Patrons.List;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using LibraryManagement.Application.Patrons.Delete;

namespace LibraryManagement.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PatronController : ApiController
	{
		private readonly ISender _sender;
		public PatronController(ISender sender)
		{
			_sender = sender;
		}

		[HttpPost("add-patron")]
		[AllowAnonymous]
		public async Task<IActionResult> Add([FromBody]CreatePatronCommand request)
		{
			var result = await _sender.Send(request);

			if(result.IsError)
			{
				return Problem(result.Errors);
			}
			return Ok(result);
		}

		[HttpGet("list-patron")]
		[AllowAnonymous]
		public async Task<IActionResult> List([FromQuery]ListPatronQuery request)
		{
			var result = await _sender.Send(request);

			if(result.IsError)
			{
				return Problem(result.Errors);
			}
			return Ok(result.Value);
		}

		[HttpGet("get-patron")]
		[AllowAnonymous]
		public async Task<IActionResult> Get([FromQuery]GetPatronQuery request)
		{
			var result = await _sender.Send(request);

			if(result.IsError)
			{
				return Problem(result.Errors);
			}
			return Ok(result.Value);
		}

		[HttpPost("delete-patron")]
		[AllowAnonymous]
		public async Task<IActionResult> Delete([FromBody] DeletePatronCommand request)
		{
			var result = await _sender.Send(request);
			
			if(result.IsError)
				return Problem(result.Errors);
			return Ok(result.Value);
		}
	}
}
