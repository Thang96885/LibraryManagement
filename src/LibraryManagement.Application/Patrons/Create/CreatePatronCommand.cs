using ErrorOr;
using LibraryManagement.Domain.PatronAggregate.ValueObjects;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Patrons.Create
{
    public record CreatePatronCommand(
        string Name,
        string Email,
        string PhoneNumber,
        PatronAddressDto Address = null) : IRequest<ErrorOr<CreatePatronResult>>;

    public record PatronAddressDto(string Street, string City, string State, string ZipCode);
    public record CreatePatronResult(Guid Id);
}
