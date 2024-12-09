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
    public record GetBookBookCopy(string Ibns, 
        DateTime AcquisitionDate, string Status, 
        decimal Price, string BookPhysicalCondition);
    public record GetBookDto(BookDto BookInfo
        , List<GetBookBookCopy> BookCopyList);
    public record GetBookQuery(int Id) : IRequest<ErrorOr<GetBookDto>>;
}
