using ErrorOr;
using LibraryManagement.Application.Books.Common;
using LibraryManagement.Domain.BookAggregate;
using LibraryManagement.Domain.Common.Interface;
using LibraryManagement.Domain.GenreAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using LibraryManagement.Domain.AuthorAggregate;
using LibraryManagement.Domain.BookAggregate.ValueObjects;
using LibraryManagement.Domain.LocationAggregate;
using LibraryManagement.Domain.YearPublicationAggregate;

namespace LibraryManagement.Application.Books.List
{
	public class ListBookQueryHandler : IRequestHandler<ListBookQuery, ErrorOr<ListBookDto>>
	{
		private readonly IBaseRepository<Book> _bookRepository;
		private readonly IBaseRepository<Genre> _genreRepository;
		private readonly IBaseRepository<Location> _locationRepository;
		private readonly IBaseRepository<Author> _authorRepository;
		private readonly IBaseRepository<PublicationYear> _publicationYearRepository;

		public ListBookQueryHandler(IBaseRepository<Book> bookRepository,
			IBaseRepository<Genre> genreRepository,
			IBaseRepository<Location> locationRepository,
			IBaseRepository<PublicationYear> publicationYearRepository, IBaseRepository<Author> authorRepository)
		{
			_bookRepository = bookRepository;
			_genreRepository = genreRepository;
			_locationRepository = locationRepository;
			_publicationYearRepository = publicationYearRepository;
			_authorRepository = authorRepository;
		}

		public async Task<ErrorOr<ListBookDto>> Handle(ListBookQuery request, CancellationToken cancellationToken)
		{
			List<Book> books;
			var totalBookCount = 0;

			if (request.BookId == 0 && String.IsNullOrEmpty((request.BookTitle)) &&
			    request.AuthorId == 0 && request.YearPublicationId == 0 && request.LocationId == 0 && 
			    request.IsAvailable == false && (request.GenreIds == null || request.GenreIds.Any() == false))
			{
				books = await _bookRepository.ListAsync(request.Page, request.PageSize);
				totalBookCount = _bookRepository.GetNumberOfEntities();
			}
			else
			{
				var queryBook = _bookRepository.GetQueryable();
				
				if(request.BookId > 0)
					queryBook = queryBook.Where(b => b.Id == request.BookId);
				if(request.AuthorId > 0)
					queryBook = queryBook.Where(b => b.AuthorIds.Select(id => id.Value).Contains(request.AuthorId));
				if(String.IsNullOrEmpty(request.BookTitle) == false)
					queryBook = queryBook.Where(b => b.Title.Contains(request.BookTitle));
				if(request.LocationId > 0)
					queryBook = queryBook.Where(b => b.LocationId == new BookLocationId(request.LocationId));
				if (request.IsAvailable == true)
					queryBook = queryBook.Where(b => b.NumberAvailable > 0);
				if (request.YearPublicationId != 0)
					queryBook = queryBook.Where(b =>
						b.PublicationYearId == BookPublicationYearId.Create(request.YearPublicationId));
				if (request.GenreIds != null && request.GenreIds.Any())
				{
					var genreIds = request.GenreIds.ToList();
					/*queryBook = queryBook.AsEnumerable()
						.Where(book => genreIds.All(genreId => book.GenreIds.Select(g => g.Value)
							.Contains(genreId))).AsQueryable();*/

					foreach (var genreId in genreIds)
					{
						queryBook = queryBook.Where(b => b.GenreIds.Select(id => id.Value).Contains(genreId));
					}
				}
				
				totalBookCount = queryBook.Count();
				books = queryBook.Skip((request.Page - 1) * request.PageSize).Take(request.PageSize).ToList();
			}
			

			var bookDtos = new List<BookDto>();

			foreach(var book in books)
			{
				var genres = new List<GenreDto>();
				foreach(var genreId in book.GenreIds)
				{
					var genreDto = await _genreRepository.FindAsync(genreId.Value);
					genres.Add(new GenreDto {Id = genreDto.Id, Name = genreDto.Name});
				}

				var location = await _locationRepository.FindAsync(book.LocationId.Value);

				var authorsName = "";

				foreach (var authorId in book.AuthorIds)
				{
					var author = await _authorRepository.FindAsync(authorId.Value);
					authorsName += author.Name + ", ";
				}
				
				var publicationYear = await _publicationYearRepository.FindAsync(book.PublicationYearId.Value);
				
				var bookDto = new BookDto
				{
					Id = book.Id,
					Title = book.Title,
					AuthorName = authorsName,
					PublisherName = book.PublisherName,
					PublicationYear = publicationYear.Year,
					PageCount = book.PageCount,
					NumberOfCopies = book.NumberOfCopy,
					Genres = genres,
					Location = new LocationDto(location.Id, location.Name),
					NumberAvailable = book.NumberAvailable,
				};
				bookDtos.Add(bookDto);
			}

			
			
			return new ListBookDto(totalBookCount, bookDtos);
		}
	}
}
