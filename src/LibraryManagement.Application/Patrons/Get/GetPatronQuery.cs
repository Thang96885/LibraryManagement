using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Patrons.Get
{
	public record GetPatronQuery(int id) : IRequest<ErrorOr<GetPatronDto>>;
}
