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
	public class Book : AggregateRoot<BookId, Guid>
	{
        private  List<BookGenreId> _genreIds = new();
        public BookId Id { get; private set; }
        public string Title { get; private set; }
        public BookAuthorId AuthorId { get;private set; }
        public String PublisherName { get;private set; }
        public int PublicationYear { get; private set; }
        public int PageCount { get; private set; }

        public List<BookGenreId> GenreIds
		{
			get => _genreIds;
			private set => _genreIds = value;
		}

        public int NumberOfCopy { get; private set; }
        public int NumberAvailable { get; private set; }
    }
}
