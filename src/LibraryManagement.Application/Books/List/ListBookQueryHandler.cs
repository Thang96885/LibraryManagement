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

namespace LibraryManagement.Application.Books.List
{
	public class ListBookQueryHandler : IRequestHandler<ListBookQuery, ErrorOr<ListBookDto>>
	{
		private readonly IBaseRepository<Book> _bookRepository;
		private readonly IBaseRepository<Genre> _genreRepository;

		public ListBookQueryHandler(IBaseRepository<Book> bookRepository, IBaseRepository<Genre> genreRepository)
		{
			_bookRepository = bookRepository;
			_genreRepository = genreRepository;
		}

		public async Task<ErrorOr<ListBookDto>> Handle(ListBookQuery request, CancellationToken cancellationToken)
		{
			List<Book> books;
			var totalBookCount = 0;

			if (request.bookId == 0 && String.IsNullOrEmpty((request.bookTitle)) &&
			    String.IsNullOrEmpty(request.authorName))
			{
				books = await _bookRepository.ListAsync(request.page, request.pageSize);
				totalBookCount = _bookRepository.GetNumberOfEntities();
			}
			else
			{
				var queryBook = _bookRepository.GetQueryable();
				
				if(request.bookId > 0)
					queryBook = queryBook.Where(b => b.Id == request.bookId);
				if(String.IsNullOrEmpty(request.authorName) == false)
					queryBook = queryBook.Where(b => b.AuthorName.Contains(request.authorName));
				if(String.IsNullOrEmpty(request.bookTitle) == false)
					queryBook = queryBook.Where(b => b.Title.Contains(request.bookTitle));
				
				totalBookCount = queryBook.Count();
				books = queryBook.Skip((request.page - 1) * request.pageSize).Take(request.pageSize).ToList();
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

			
			
			return new ListBookDto(totalBookCount, bookDtos);
		}
	}
}
