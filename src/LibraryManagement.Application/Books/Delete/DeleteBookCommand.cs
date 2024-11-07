using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Books.Delete
{
    public record DeleteBookCommand(int Id) : IRequest<ErrorOr<string>>;
}
