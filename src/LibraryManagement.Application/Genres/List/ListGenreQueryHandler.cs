using ErrorOr;
using LibraryManagement.Domain.Common.Interface;
using LibraryManagement.Domain.GenreAggregate;
using MapsterMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagement.Application.Books.Common;

namespace LibraryManagement.Application.Genres.List
{
	public class ListGenreQueryHandler
		: IRequestHandler<ListGenreQuery, ErrorOr<ListGenreDto>>
	{
		private readonly IBaseRepository<Genre> _genreRepository;
		private readonly IMapper _mapper;

		public ListGenreQueryHandler(IBaseRepository<Genre> genreRepository, IMapper mapper)
		{
			_genreRepository = genreRepository;
			_mapper = mapper;
		}

		public async Task<ErrorOr<ListGenreDto>> Handle(ListGenreQuery request, CancellationToken cancellationToken)
		{
			
			var genres = new List<Genre>();

			if (request.genreId == 0 && request.SearchName == "")
			{
				genres = await _genreRepository.ListAsync();
			}
			else
			{
				var genreQuery = _genreRepository.GetQueryable();
				if (request.genreId != 0)
					genreQuery = genreQuery.Where(g => g.Id == request.genreId);
				if (request.SearchName != "")
					genreQuery = genreQuery.Where(g => g.Name.Contains(request.SearchName));

				genres = genreQuery.ToList();
			}
			var totalCount = genres.Count();

			var result = new ListGenreDto(
				genres.Select(g => new ListGenreRecord(g.Id, g.Name, g.BookIds.Count)).ToList(), totalCount);

			return result;
		}
	}
}
