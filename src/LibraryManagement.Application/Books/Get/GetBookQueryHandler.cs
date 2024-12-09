using ErrorOr;
using LibraryManagement.Application.Books.Common;
using LibraryManagement.Domain.BookAggregate;
using LibraryManagement.Domain.Common.Interface;
using LibraryManagement.Domain.GenreAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using LibraryManagement.Domain.AuthorAggregate;
using LibraryManagement.Domain.LocationAggregate;
using LibraryManagement.Domain.YearPublicationAggregate;

namespace LibraryManagement.Application.Books.Get
{
    public class GetBookQueryHandler : IRequestHandler<GetBookQuery, ErrorOr<GetBookDto>>
	{
		private readonly IBaseRepository<Book> _bookReposiotry;
		private readonly IBaseRepository<Genre> _genreRepository;
		private readonly IBaseRepository<Location> _locationRepository;
		private readonly IBaseRepository<Author> _authorRepository;
		private readonly IBaseRepository<PublicationYear> _publicationYearRepository;

		public GetBookQueryHandler(IBaseRepository<Book> bookReposiotry, IBaseRepository<Genre> genreRepository, IBaseRepository<Location> locationRepository, IBaseRepository<Author> authorRepository, IBaseRepository<PublicationYear> publicationYearRepository)
		{
			_bookReposiotry = bookReposiotry;
			_genreRepository = genreRepository;
			_locationRepository = locationRepository;
			_authorRepository = authorRepository;
			_publicationYearRepository = publicationYearRepository;
		}

		public async Task<ErrorOr<GetBookDto>> Handle(GetBookQuery request, CancellationToken cancellationToken)
		{
			var book = await _bookReposiotry.FindAsync(request.Id);

			if (book == null)
				return Error.NotFound();

			var genres = new List<GenreDto>();
			foreach (var genreId in book.GenreIds)
			{
				var genre = await _genreRepository.FindAsync(genreId.Value);

				genres.Add(new GenreDto { Id = genre.Id, Name = genre.Name });
			}

			var location = await _locationRepository.FindAsync(book.LocationId.Value);
			var publicationYear = await _publicationYearRepository.FindAsync(book.PublicationYearId.Value);
			var authors = new List<AuthorDto>();
			foreach (var authorId in book.AuthorIds)
			{
				var author = await _authorRepository.FindAsync(authorId.Value);
				authors.Add(new AuthorDto(author.Id, author.Name));
			}

			var bookInfo = new BookDto
			{
				Id = book.Id,
				Title = book.Title,
				Authors = authors,
				ImageUrl = book.ImageUrl,
				PublisherName = book.PublisherName,
				PublicationYear = new PublicationYearDto(publicationYear.Id, publicationYear.Year),
				PageCount = book.PageCount,
				NumberOfCopies = book.NumberOfCopy,
				NumberAvailable = book.NumberAvailable,
				Genres = genres,
				Description = book.Description,
				Location = new LocationDto(location.Id, location.Name)
			};
			
			return new GetBookDto(bookInfo, book.BookCopies.Select(copy => new GetBookBookCopy(
				copy.Id, copy.AcquisitionDate,
				copy.Status.ToString(), copy.Price, copy.PhysicalCondition.ToString())).ToList());
		}
	}
}
