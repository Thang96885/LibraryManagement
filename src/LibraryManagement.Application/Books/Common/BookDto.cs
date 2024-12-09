using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Books.Common
{
	public record BookDto
	{
		public int Id { get; init; }
		public string Title { get; init; }
		public string PublisherName { get; init; }
		public int PageCount { get; init; }
		public int NumberOfCopies { get; init; }
		public int NumberAvailable { get; init; }
		public string Description { get; init; }
		public string ImageUrl { get; init; }
		public PublicationYearDto PublicationYear { get; init; }
		public ICollection<AuthorDto> Authors { get; init; }
		public LocationDto Location { get; init; }
		public ICollection<GenreDto> Genres { get; init; }
	}

	public record PublicationYearDto(int Id, int Year);

	public record AuthorDto(int Id, string Name);

	public record GenreDto
	{
		public int Id { get; init; }
		public string Name { get; init; }
	}

	public record LocationDto(int Id, string Name);

}
