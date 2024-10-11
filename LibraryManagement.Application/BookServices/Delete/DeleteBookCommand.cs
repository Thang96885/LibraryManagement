using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.BookServices.Delete
{
	public record DeleteBookCommand(String Id) : IRequest<ErrorOr<string>>;
}
