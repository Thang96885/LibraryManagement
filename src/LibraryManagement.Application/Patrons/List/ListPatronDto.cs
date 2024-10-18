using System;
using System.Collections.Generic;

namespace LibraryManagement.Application.Patrons.List
{
	public record ListPatronDto(string Id,
		string Name,
		string Email,
		string PhoneNumber, string Street, string City, string State,
		string BorrowRecordCount, string ReturnRecordCount);
}
