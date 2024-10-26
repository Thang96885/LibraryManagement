using LibraryManagement.Application.Genres.List;
using LibraryManagement.Domain.GenreAggregate;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagement.Application.Genres.Get;

namespace LibraryManagement.Application.Genres.Common.MappingConfiguration
{
	public class GenreMappingConfiguration : IRegister
	{
		public void Register(TypeAdapterConfig config)
		{
			config.NewConfig<Genre, ListGenreDto>()
				.Map(dest => dest.NumberBook, src => src.BookIds.Count());

			config.NewConfig<Genre, GetGenreDto>()
				.Map(dest => dest.NumberOfBooks, src => src.BookIds.Count());
		}
	}
}
