using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Books.AddBookCopy
{
	public record AddBookCopyCommand(int BookId, List<string> IBNSCodes) : IRequest<ErrorOr<List<string>>>;
}
