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
        string PublisherName,
        int PublicationYearId,
        int PageCount,
        int AuthorId,
        string ImageUrl,
        string Description,
        int LocationId): IRequest<ErrorOr<string>>;
}
