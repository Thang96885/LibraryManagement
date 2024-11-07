using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Patrons.Get
{
	public record GetPatronDto(int Id, string Name,
		string Email, string PhoneNumber, string Street,
		string City, string State, string ZipCode,
		int ReservationCount, int BorrowRecordCount,
		int ReturnRecordCount);
}
