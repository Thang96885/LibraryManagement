using ErrorOr;
using LibraryManagement.Domain.Common.Interface;
using LibraryManagement.Domain.GenreAggregate;
using MapsterMapper;
using MediatR;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using LibraryManagement.Domain.Common.Interface.DomainServices;

namespace LibraryManagement.Application.Genres.Create
{
	public class CreateGenreCommandHandler : IRequestHandler<CreateGenreCommand, ErrorOr<CreateGenreDto>>
	{
		private readonly IBaseRepository<Genre> _genreRepository;
		private readonly IMapper _mapper;
		private readonly IGenreService _genreService;

		public CreateGenreCommandHandler(IBaseRepository<Genre> genreRepository, IMapper mapper, IGenreService genreService)
		{
			_genreRepository = genreRepository;
			_mapper = mapper;
			_genreService = genreService;
		}

		public async Task<ErrorOr<CreateGenreDto>> Handle(CreateGenreCommand request,
			CancellationToken cancellationToken)
		{
			try
			{
				var genre = Genre.Create(request.genreName, _genreService);

				_genreRepository.Add(genre);

				await _genreRepository.SaveChangeAsync();

				return _mapper.Map<CreateGenreDto>(genre);
			}
			catch (DuplicateNameException ex)
			{
				return Error.Conflict(ex.Message);
			}
			
			catch (Exception ex)
			{
				// Handle other exceptions
				return Error.Failure("An error occurred while creating genre");
			}
		}
	}
}
