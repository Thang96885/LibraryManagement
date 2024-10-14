using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Books.Create
{
    public class CreateBookValidator : AbstractValidator<CreateBookCommand>
    {
        public CreateBookValidator()
        {
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.PublisherName).NotEmpty();
            RuleFor(x => x.PublicationYear).NotEmpty();
            RuleFor(x => x.PageCount).NotEmpty().GreaterThan(0);
            RuleFor(x => x.NumberOfCopy).NotEmpty().GreaterThanOrEqualTo(0);
        }
    }
}
