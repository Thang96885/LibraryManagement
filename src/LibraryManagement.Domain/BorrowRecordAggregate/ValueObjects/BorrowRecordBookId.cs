using LibraryManagement.Domain.Common.BaseModels;
using LibraryManagement.Domain.GenreAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Domain.BorrowRecordAggregate.ValueObjects
{
	public class BorrowRecordBookId : ValueObject
	{
		public Guid BookId { get; private set; }
		public List<string> BookCopyIds { get; private set; }
		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return BookId;

			foreach (var bookCopyId in BookCopyIds)
			{
				yield return bookCopyId;
			}
		}

		private BorrowRecordBookId(Guid bookId, List<string> bookCopyIds)
		{
			BookId = bookId;
			BookCopyIds = bookCopyIds;
		}

		public static BorrowRecordBookId Create(Guid bookId, List<string> bookCopyIds)
		{
			return new(bookId, bookCopyIds);
		}
	}
}
