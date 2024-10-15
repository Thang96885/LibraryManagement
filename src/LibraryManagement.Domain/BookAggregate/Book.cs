﻿
using LibraryManagement.Domain.BookAggregate.Entities;
using LibraryManagement.Domain.BookAggregate.ValueObjects;
using LibraryManagement.Domain.Common.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private Book(Guid Id, string title, string authorName, string publisherName, 
			int publicationYear, int pageCount, int numberOfCopy, int numberAvailable)
		{
			this.Id = Id;
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
			return new Book(Guid.NewGuid(), title, authorName, publisherName, publicationYear, pageCount, numberOfCopy, numberAvailable);
		}

        public void Delete()
        {
            
        }
    }
}