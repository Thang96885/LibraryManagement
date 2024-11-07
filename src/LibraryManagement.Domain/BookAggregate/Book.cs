
using LibraryManagement.Domain.BookAggregate.Entities;
using LibraryManagement.Domain.BookAggregate.ValueObjects;
using LibraryManagement.Domain.Common.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using LibraryManagement.Domain.BookAggregate.Events;
using LibraryManagement.Domain.Common.Enums;

namespace LibraryManagement.Domain.BookAggregate
{
	//Book information includes: ISBN, title, author, publisher, publication year, page count, genre, number of copies
	public class Book : AggregateRoot
	{
        private readonly List<BookGenreId> _genreIds = new();
        private readonly List<BookBorrowRecordId> _borrowRecordIds = new();
        private readonly List<BookReturnRecordId> _returnRecordIds = new();
        private readonly List<BookReservationId> _bookReservationId = new();
		private readonly List<BookCopy> _bookCopies = new();
        public string Title { get; private set; }
        public string AuthorName { get;private set; }
        public string PublisherName { get;private set; }
        public int PublicationYear { get; private set; }
        public int PageCount { get; private set; }
		public int NumberOfCopy { get; private set; }
		public int NumberAvailable { get; private set; }

		public IReadOnlyList<BookGenreId> GenreIds { get => _genreIds.AsReadOnly(); }
        public IReadOnlyList<BookBorrowRecordId> BorrowRecordIds { get => _borrowRecordIds.AsReadOnly(); }
        public IReadOnlyList<BookReturnRecordId> ReturnRecordIds { get => _returnRecordIds.AsReadOnly(); }
        public IReadOnlyList<BookReservationId> BookReservationId { get => _bookReservationId.AsReadOnly(); }
        public IReadOnlyList<BookCopy> BookCopies => _bookCopies.AsReadOnly();

        private Book(string title, string authorName, string publisherName, 
			int publicationYear, int pageCount, int numberOfCopy, int numberAvailable)
        {
			Title = title;
			AuthorName = authorName;
			PublisherName = publisherName;
			PublicationYear = publicationYear;
			PageCount = pageCount;
			NumberOfCopy = numberOfCopy;
			NumberAvailable = numberAvailable;
		}
		private Book()
		{

		}

        public static Book Create(string title, string authorName, string publisherName, 
			int publicationYear, int pageCount, int numberOfCopy, int numberAvailable)
		{
			return new Book(title, authorName, publisherName, publicationYear, pageCount, numberOfCopy, numberAvailable);
		}

		public void AddBookCopy(BookCopy bookCopy)
		{
			this._bookCopies.Add(bookCopy);
			this.NumberAvailable++;
			this.NumberOfCopy++;
		}

		public void UpdateBookInfo(string title = "", string authorName = "", string publisherName = "", int publicationYear = 0, int pageCount = 0)
		{
			this.Title = String.IsNullOrEmpty(title) ? Title : title;
			this.AuthorName = String.IsNullOrEmpty(authorName) ? AuthorName : authorName;
			this.PublisherName = String.IsNullOrEmpty(publisherName) ? PublisherName : publisherName;
			this.PublicationYear = publicationYear == 0 ? PublicationYear : publicationYear; 
			this.PageCount = pageCount == 0 ? PageCount : pageCount;
		}

		public void BorrowBook(List<string> bookCopyIds)
		{
			var bookCopies = _bookCopies.Where(bc => bookCopyIds.Contains(bc.Id)).ToList();

			if (bookCopies.Count == 0)
				return;

			foreach (var bookCopy in bookCopies)
			{
				bookCopy.ChangeStatus(BookStatus.Borrowed);
			}

			this.NumberAvailable -= bookCopies.Count;
		}

		public void UpdateGenre(List<BookGenreId> addBookGenreIds, List<BookGenreId> removeBookGenreIds)
		{
			
			var updatedBookGenre = new UpdatedBookGenres(this.Id, addBookGenreIds.Except(this._genreIds).ToList(),
				removeBookGenreIds.Intersect(_genreIds).ToList());
			
			_genreIds.AddRange(updatedBookGenre.addedBookGenreIds);

			foreach (var genreId in updatedBookGenre.removedGenreIds)
			{
				_genreIds.Remove(genreId);
			}
			
			this.AddDomainEvent(updatedBookGenre);
		}

        public void Delete()
        {
            
        }
    }
}
