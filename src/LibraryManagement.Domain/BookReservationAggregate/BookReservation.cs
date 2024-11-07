using LibraryManagement.Domain.BookReservationAggregate.ValueObjects;
using LibraryManagement.Domain.Common.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Domain.BookReservationAggregate
{
    public class BookReservation : AggregateRoot
	{
		private readonly List<ReservationBookCopyId> _bookIds = new();
		public BookReservationPatronId PatronId { get; private set; }
		public IReadOnlyList<ReservationBookCopyId> BookId => _bookIds.AsReadOnly();
		public DateTime ReservationDate { get; private set; }
		public DateTime? ReservationExpirationDate { get; private set; }
		public bool IsReserved { get; private set; }
		private BookReservation(int patronId, DateTime reservationDate, DateTime? reservationExpirationDate)
		{
			PatronId = BookReservationPatronId.Create(patronId);
			ReservationDate = reservationDate;
			ReservationExpirationDate = reservationExpirationDate;
		}

		public static BookReservation Create(int patronId, DateTime reservationDate, DateTime? reservationExpirationDate)
		{
			return new(patronId, reservationDate, reservationExpirationDate);
		}

		private BookReservation()
		{

		}
	}
}
