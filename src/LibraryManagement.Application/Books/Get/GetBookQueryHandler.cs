using ErrorOr;
using LibraryManagement.Application.Books.Common;
using LibraryManagement.Domain.BookAggregate;
using LibraryManagement.Domain.Common.Interface;
using LibraryManagement.Domain.GenreAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Books.Get
{
    public class GetBookQueryHandler : IRequestHandler<GetBookQuery, ErrorOr<BookDto>>
	{
		private readonly IBaseRepository<Book> _bookReposiotry;
		private readonly IBaseRepository<Genre> _genreRepository;

		public GetBookQueryHandler(IBaseRepository<Book> bookReposiotry, IBaseRepository<Genre> genreRepository)
		{
			_bookReposiotry = bookReposiotry;
			_genreRepository = genreRepository;
		}

		public async Task<ErrorOr<BookDto>> Handle(GetBookQuery request, CancellationToken cancellationToken)
		{
			var book = await _bookReposiotry.FindAsync(request.Id);

			if (book == null)
				return Error.NotFound();

			var genres = new List<GenreDto>();
			foreach(var genreId in book.GenreIds)
			{
				var genre = await _genreRepository.FindAsync(genreId.Value);

				genres.Add(new GenreDto { Id = genre.Id, Name = genre.Name });
			}

			return new BookDto
			{
				Id = book.Id,
				Title = book.Title,
				AuthorName = book.AuthorName,
				PublisherName = book.PublisherName,
				PublicationYear = book.PublicationYear,
				PageCount = book.PageCount,
				NumberOfCopy = book.NumberOfCopy,
				NumberAvailable = book.NumberAvailable,
				Genres = genres
			};
		}


	}
}
