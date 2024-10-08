
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
        public string Title { get; private set; }
        public Guid? AuthorId { get;private set; }
        public string PublisherName { get;private set; }
        public int PublicationYear { get; private set; }
        public int PageCount { get; private set; }
		public int NumberOfCopy { get; private set; }
		public int NumberAvailable { get; private set; }

		public IReadOnlyList<BookGenreId> GenreIds { get => _genreIds.AsReadOnly(); }
        public IReadOnlyList<BookBorrowRecordId> BorrowRecordIds { get => _borrowRecordIds.AsReadOnly(); }
        public IReadOnlyList<BookReturnRecordId> ReturnRecordIds { get => _returnRecordIds.AsReadOnly(); }
        public IReadOnlyList<BookReservationId> BookReservationId { get => _bookReservationId.AsReadOnly(); }

        private Book(Guid Id, string title, Guid authorId, string publisherName, 
			int publicationYear, int pageCount, int numberOfCopy, int numberAvailable)
		{
			this.Id = Id;
			Title = title;
			AuthorId = authorId;
			PublisherName = publisherName;
			PublicationYear = publicationYear;
			PageCount = pageCount;
			NumberOfCopy = numberOfCopy;
			NumberAvailable = numberAvailable;
		}
		private Book()
		{

		}

        public static Book Create(string title, Guid authorId, string publisherName, 
			int publicationYear, int pageCount, int numberOfCopy, int numberAvailable)
		{
			return new Book(Guid.NewGuid(), title, authorId, publisherName, publicationYear, pageCount, numberOfCopy, numberAvailable);
		}
    }
}
