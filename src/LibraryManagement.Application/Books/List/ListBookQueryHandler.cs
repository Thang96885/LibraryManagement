using ErrorOr;
using LibraryManagement.Application.Books.Common;
using LibraryManagement.Domain.BookAggregate;
using LibraryManagement.Domain.Common.Interface;
using LibraryManagement.Domain.GenreAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Books.List
{
	public class ListBookQueryHandler : IRequestHandler<ListBookQuery, ErrorOr<List<BookDto>>>
	{
		private readonly IBaseRepository<Book> _bookRepository;
		private readonly IBaseRepository<Genre> _genreRepository;

		public ListBookQueryHandler(IBaseRepository<Book> bookRepository, IBaseRepository<Genre> genreRepository)
		{
			_bookRepository = bookRepository;
			_genreRepository = genreRepository;
		}

		public async Task<ErrorOr<List<BookDto>>> Handle(ListBookQuery request, CancellationToken cancellationToken)
		{
			var books = await _bookRepository.ListAsync();

			var bookDtos = new List<BookDto>();

			foreach(var book in books)
			{
				var genres = new List<GenreDto>();
				foreach(var genreId in book.GenreIds)
				{
					var genreDto = await _genreRepository.FindAsync(genreId.Value);
					genres.Add(new GenreDto {Id = genreDto.Id, Name = genreDto.Name});
				}
				var bookDto = new BookDto
				{
					Id = book.Id,
					Title = book.Title,
					AuthorName = book.AuthorName,
					PublisherName = book.PublisherName,
					PublicationYear = book.PublicationYear,
					PageCount = book.PageCount,
					NumberOfCopy = book.NumberOfCopy,
					Genres = genres
				};
				bookDtos.Add(bookDto);
			}
			
			return bookDtos;
		}
	}
}
