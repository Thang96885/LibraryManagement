using LibraryManagement.Domain.Common.BaseModels;
using LibraryManagement.Domain.PatronAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Domain.PatronAggregate
{
	public class Patron : AggregateRoot
	{
		private readonly List<PatronReservationId> _reservationIds = new();
		private readonly List<PatronBorrowRecordId> _borrowRecordIds = new();
		private readonly List<PatronReturnRecordId> _returnRecordIds = new();	
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string PhoneNumber { get; private set; }
		public PatronAddress Address { get; private set; }
		public DateTime RegistrationDate { get; private set; }
		public IReadOnlyList<PatronReservationId> ReservationIds => _reservationIds.AsReadOnly();
		public IReadOnlyList<PatronBorrowRecordId> BorrowRecordIds => _borrowRecordIds.AsReadOnly();
		public IReadOnlyList<PatronReturnRecordId> ReturnRecordIds => _returnRecordIds.AsReadOnly();

		private Patron(Guid id, string name, string email, string phoneNumber, PatronAddress address, DateTime registrationDate)
		{
			Id = id;
			Name = name;
			Email = email;
			PhoneNumber = phoneNumber;
			Address = address;
			RegistrationDate = registrationDate;
		}

		public static Patron Create(string name, string email, string phoneNumber, PatronAddress address)
		{
			return new(Guid.NewGuid(), name, email, phoneNumber, address, DateTime.UtcNow);
		}
    }
}
