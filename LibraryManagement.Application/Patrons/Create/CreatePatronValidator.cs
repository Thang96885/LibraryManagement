using FluentValidation;
using LibraryManagement.Domain.PatronAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Patrons.Create
{
	public class CreatePatronValidator : AbstractValidator<CreatePatronCommand>
	{
		public CreatePatronValidator()
		{
			RuleFor(x => x.Name).NotEmpty();
			RuleFor(x => x.Email).NotEmpty().EmailAddress();
			RuleFor(x => x.PhoneNumber).NotEmpty();
			RuleFor(x => x.Address).SetValidator(new PatronAddressDtoValidator());
		}
	}

	public class PatronAddressDtoValidator : AbstractValidator<PatronAddressDto>
	{
		public PatronAddressDtoValidator()
		{
			RuleFor(x => x.Street).NotEmpty();
			RuleFor(x => x.City).NotEmpty();
			RuleFor(x => x.State).NotEmpty();
			RuleFor(x => x.ZipCode).NotEmpty();
		}
	}
}
