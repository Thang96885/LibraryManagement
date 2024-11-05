using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Api.Controllers
{
	[ApiController]
	
	public class ApiController : ControllerBase
	{
		protected IActionResult Problem(List<Error> errors)
		{
			if (errors.Count == 0)
			{
				return Problem();
			}
			if (errors.All(e => e.Type == ErrorType.Validation))
			{
				return ValidationProblem(errors);
			}

			var error = errors.First();
			
			return Problem(error);
		}

		private IActionResult ValidationProblem(List<Error> errors)
		{
			var modelStateDic = new ModelStateDictionary();

			foreach (var error in errors)
			{
				modelStateDic.AddModelError(error.Code, error.Description);
			}
			return ValidationProblem(modelStateDic);
		}

		private IActionResult Problem(Error error)
		{
			switch (error.Type)
			{
				case ErrorType.NotFound:
					return NotFound(error.Code);
				case ErrorType.Conflict:
					return Conflict(error.Code);
				default:
					return Problem(error.Code);
			}
		}
	}
}
