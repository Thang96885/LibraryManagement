using ErrorOr;
using LibraryManagement.Domain.Common.Interface;
using LibraryManagement.Domain.PatronAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Patrons.List
{
	public record ListPatronQuery(int page, int pageSize) : IRequest<ErrorOr<List<ListPatronDto>>>; 
}
