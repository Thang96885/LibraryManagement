using ErrorOr;
using LibraryManagement.Domain.Common.Interface;
using LibraryManagement.Domain.GenreAggregate;
using MapsterMapper;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Genres.Create
{
	public class CreateGenreCommandHandler : IRequestHandler<CreateGenreCommand, ErrorOr<CreateGenreDto>>
	{
		private readonly IBaseRepository<Genre> _genreRepository;
		private readonly IMapper _mapper;

		public CreateGenreCommandHandler(IBaseRepository<Genre> genreRepository, IMapper mapper)
		{
			_genreRepository = genreRepository;
			_mapper = mapper;
		}

		public async Task<ErrorOr<CreateGenreDto>> Handle(CreateGenreCommand request, CancellationToken cancellationToken)
		{
			try
			{
				var genre = Genre.Create(request.genreName);

				_genreRepository.Add(genre);

				await _genreRepository.SaveChangeAsync();

				return _mapper.Map<CreateGenreDto>(genre);
			}
			catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx && (sqlEx.Number == 2601 || sqlEx.Number == 2627))
			{
				// Handle unique constraint violation
				return Error.Conflict("Genre name already exists");
			}
			catch (Exception ex)
			{
				// Handle other exceptions
				return Error.Failure("An error occurred while creating genre");
			}
		}
	}
}
