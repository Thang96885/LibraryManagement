using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Genres.List
{
	public record ListGenreDto(Guid Id, string Name, int NumberBook);

	public record ListGenreQuery(int page, int pageSize) : IRequest<ErrorOr<List<ListGenreDto>>>;
}
