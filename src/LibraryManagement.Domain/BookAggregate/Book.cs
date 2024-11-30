
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
        private readonly List<BookAuthorId> _authorIds = new();
        private readonly List<BookBorrowRecordId> _borrowRecordIds = new();
        private readonly List<BookReturnRecordId> _returnRecordIds = new();
        private readonly List<BookReservationId> _bookReservationId = new();
		private readonly List<BookCopy> _bookCopies = new();
        public string Title { get; private set; }
        public string PublisherName { get;private set; }
        public BookPublicationYearId PublicationYearId { get; private set; }
        public int PageCount { get; private set; }
		public int NumberOfCopy { get; private set; }
		public int NumberAvailable { get; private set; }
		public string ImageUrl { get; private set; }
		public string Description { get; private set; }
		public BookLocationId? LocationId { get; private set; }
		public List<BookAuthorId> AuthorIds => _authorIds;

		public IReadOnlyList<BookGenreId> GenreIds { get => _genreIds.AsReadOnly(); }
        public IReadOnlyList<BookBorrowRecordId> BorrowRecordIds { get => _borrowRecordIds.AsReadOnly(); }
        public IReadOnlyList<BookReturnRecordId> ReturnRecordIds { get => _returnRecordIds.AsReadOnly(); }
        public IReadOnlyList<BookReservationId> BookReservationId { get => _bookReservationId.AsReadOnly(); }
        public IReadOnlyList<BookCopy> BookCopies => _bookCopies.AsReadOnly();

        private Book(string title, List<int> authorIds, string publisherName, 
			int publicationYearId, int pageCount, int numberOfCopy,
			int numberAvailable, string imageUrl, string description, int locationId)
        {
			Title = title;
			_authorIds = authorIds.Select(authorId => BookAuthorId.Create(authorId)).ToList();
			PublisherName = publisherName;
			PublicationYearId = new BookPublicationYearId(publicationYearId);
			PageCount = pageCount;
			NumberOfCopy = numberOfCopy;
			NumberAvailable = numberAvailable;
			ImageUrl = imageUrl;
			Description = description;
			LocationId = new BookLocationId(locationId);
		}
		private Book()
		{

		}

        public static Book Create(string title, List<int> authorIds, string publisherName, 
			int publicationYearId, int pageCount, int numberOfCopy,
			int numberAvailable, string imageUrl, string description, int locationId)
		{
			var book = new Book(title, authorIds, publisherName, publicationYearId,
				pageCount, numberOfCopy, numberAvailable, imageUrl, description, locationId);
			book.AddDomainEvent(new CreatedBook(book));
			return book;
		}

		public void AddBookCopy(BookCopy bookCopy)
		{
			this._bookCopies.Add(bookCopy);
			this.NumberAvailable++;
			this.NumberOfCopy++;
		}

		public void UpdateBookInfo(string title = "",
			string publisherName = "", int publicationYearId = 0,
			int pageCount = 0, int locationId = 0,
			List<BookAuthorId> removeAuthorIds = null, List<BookAuthorId> addAuthorIds = null)
		{
			AddDomainEvent(new UpdatedBook(this.Id, publicationYearId, locationId,
				removeAuthorIds?.Select(id => id.Value).ToList(),
				addAuthorIds?.Select(id => id.Value).ToList()));
			
			this.Title = String.IsNullOrEmpty(title) ? Title : title;
			this.PublisherName = String.IsNullOrEmpty(publisherName) ? PublisherName : publisherName;
			this.PageCount = pageCount == 0 ? PageCount : pageCount;

			if (publicationYearId != 0 &&
			    this.PublicationYearId != BookPublicationYearId.Create(publicationYearId))
			{
				this.PublicationYearId = BookPublicationYearId.Create(publicationYearId);
			}
			if (locationId != 0 && this.LocationId != BookLocationId.Create(locationId))
			{
				this.LocationId = BookLocationId.Create(locationId);
			}

			if (removeAuthorIds != null && removeAuthorIds.Count > 0)
			{
				foreach (var removeAuthorId in removeAuthorIds)
				{
					if(_authorIds.Contains(removeAuthorId))
						_authorIds.Remove(removeAuthorId);
				}
			}

			if (addAuthorIds != null && addAuthorIds.Count > 0)
			{
				foreach (var bookAuthorId in addAuthorIds)
				{
					if(_authorIds.Contains(bookAuthorId))
						_authorIds.Add(bookAuthorId);
				}
			}
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

		public void DeletedLocation()
		{
			this.LocationId = null;
		}

		public void DeletedGenre(BookGenreId genreId)
		{
			this._genreIds.Remove(genreId);
		}

        public void Delete()
        {
            
        }
    }
}
