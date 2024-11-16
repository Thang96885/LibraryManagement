using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Genres.List
{
	public record ListGenreRecord(int Id, string Name, int NumberBook);
	public record ListGenreDto(List<ListGenreRecord> Genres, int NumberGenres);
	public record ListGenreQuery(int page, int pageSize, int genreId = 0, string SearchName = "") : IRequest<ErrorOr<ListGenreDto>>;
}
