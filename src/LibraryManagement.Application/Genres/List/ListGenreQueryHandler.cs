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

namespace LibraryManagement.Application.Genres.List
{
	public class ListGenreQueryHandler
		: IRequestHandler<ListGenreQuery, ErrorOr<List<ListGenreDto>>>
	{
		private readonly IBaseRepository<Genre> _genreRepository;
		private readonly IMapper _mapper;

		public ListGenreQueryHandler(IBaseRepository<Genre> genreRepository, IMapper mapper)
		{
			_genreRepository = genreRepository;
			_mapper = mapper;
		}

		public async Task<ErrorOr<List<ListGenreDto>>> Handle(ListGenreQuery request, CancellationToken cancellationToken)
		{
			var genres = await _genreRepository.ListAsync(request.page, request.pageSize);

			var genreDtos = genres.Select(genre => _mapper.Map<Genre, ListGenreDto>(genre)).ToList();

			return genreDtos;
		}
	}
}
