using ErrorOr;
using LibraryManagement.Application.Books.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Books.Get
{
    public record GetBookQuery(string Id) : IRequest<ErrorOr<BookDto>>;
}
