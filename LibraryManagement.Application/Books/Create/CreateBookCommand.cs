using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Books.Create
{
    public record CreateBookCommand(
        string Title,
        string AuthorName,
        string PublisherName,
        int PublicationYear,
        int PageCount,
        int NumberOfCopy) : IRequest<ErrorOr<string>>;
}
