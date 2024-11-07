using LibraryManagement.Application.Auth.AddRole;
using LibraryManagement.Application.Auth.Login;
using LibraryManagement.Application.Auth.Refresh;
using LibraryManagement.Application.Auth.Register;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using LibraryManagement.Application.Auth.ChangePassword;

namespace LibraryManagement.Api.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class AuthController : ApiController
	{
		private readonly ISender _sender;

		public AuthController(ISender sender)
		{
			_sender = sender;
		}

		[HttpPost("register")]
		[AllowAnonymous]
		public async Task<IActionResult> Register([FromBody] RegisterCommand request)
		{
			var result = await _sender.Send(request);

			if (result.IsError)
			{
				return Problem(result.Errors);
			}
			return Ok(result.Value);
		}

		[HttpPost("change-password")]
		public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand request)
		{
			var result = await _sender.Send(request);
			
			if(result.IsError)
				return Problem(result.Errors);
			
			return Ok(result.Value);
		}

		[HttpPost("refresh")]
		[AllowAnonymous]
		public async Task<IActionResult> Refresh(RefreshCommand request)
		{
			var result = await _sender.Send(request);
			if (result.IsError)
				return Problem(result.Errors);
			return Ok(result.Value);
		}

		[HttpPost("login")]
		[AllowAnonymous]
		public async Task<IActionResult> Login([FromBody] LoginCommand request)
		{
			var result = await _sender.Send(request);
			if (result.IsError)
				return Problem(result.Errors);
			return Ok(result.Value);
		}

		[HttpPost("add-role")]
		[AllowAnonymous]
		public async Task<IActionResult> AddRole([FromBody] AddRoleCommand request)
		{
			var result = await _sender.Send(request);

			if (result.IsError)
				return Problem(result.Errors);
			return Ok(result.Value);
		}
	}
}
