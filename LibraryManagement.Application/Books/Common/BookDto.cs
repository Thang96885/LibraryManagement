using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Books.Common
{
	public record BookDto
	{
		public Guid Id { get; init; }
		public string Title { get; init; }
		public string AuthorName { get; init; }
		public string PublisherName { get; init; }
		public int PublicationYear { get; init; }
		public int PageCount { get; init; }
		public int NumberOfCopy { get; init; }
		public int NumberAvailable { get; init; }
		public ICollection<GenreDto> Genres { get; init; }
	}

	public record GenreDto
	{
		public Guid Id { get; init; }
		public string Name { get; init; }
	}

}
