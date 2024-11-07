using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Genres.Create
{
	public record CreateGenreDto(int Id, string Name);

	public record CreateGenreCommand(string genreName) : IRequest<ErrorOr<CreateGenreDto>>;
}
