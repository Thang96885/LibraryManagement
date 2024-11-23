using LibraryManagement.Domain.Common.BaseModels;
using LibraryManagement.Domain.PatronAggregate.Events;
using LibraryManagement.Domain.PatronAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagement.Domain.BookAggregate.Events;
using LibraryManagement.Domain.BorrowRecordAggregate;
using LibraryManagement.Domain.PatronTypeAggregate.ValueObjects;

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

		public PatronPatronTypeId TypeId { get; private set; }
		public IReadOnlyList<PatronReservationId> ReservationIds => _reservationIds.AsReadOnly();
		public IReadOnlyList<PatronBorrowRecordId> BorrowRecordIds => _borrowRecordIds.AsReadOnly();
		public IReadOnlyList<PatronReturnRecordId> ReturnRecordIds => _returnRecordIds.AsReadOnly();

		private Patron(
			string name, string email,
			string phoneNumber, PatronAddress address, 
			DateTime registrationDate, PatronPatronTypeId typeId)
		{
			Name = name;
			Email = email;
			PhoneNumber = phoneNumber;
			Address = address;
			RegistrationDate = registrationDate;
			TypeId = typeId;
		}

		public static Patron Create(string name, string email, 
			string phoneNumber, PatronAddress address, PatronPatronTypeId typeId 
			)
		{
			var patron = new Patron(name, email, phoneNumber, address, DateTime.UtcNow, typeId);
			patron.AddDomainEvent(new CreatedPatron(patron));
			return patron;
		}

		public void AddBorrowRecordId(int borrowRecordId)
		{
			_borrowRecordIds.Add(PatronBorrowRecordId.Create(borrowRecordId));
		}

		public void Delete()
		{
			this.AddDomainEvent(new DeletedPatron(this));
		}

		public void Update(string Name, string Email, string PhoneNumber, string address,
			int typeId)
		{
			if(string.IsNullOrEmpty(Name) == false)
				this.Name = Name;
			if(string.IsNullOrEmpty(Email) == false)
				this.Email = Email;
			if(string.IsNullOrEmpty(PhoneNumber) == false)
				this.PhoneNumber = PhoneNumber;
			if (string.IsNullOrEmpty(address) == false)
				this.Address = PatronAddress.Create("", address, "", "");
			if (typeId != 0 && typeId != this.TypeId.Value)
			{
				this.AddDomainEvent(new UpdatedPatronType(this.Id, this.TypeId.Value, typeId));
				this.TypeId = PatronPatronTypeId.Create(typeId);
			}
		}
		private Patron()
		{

		}
    }
}
