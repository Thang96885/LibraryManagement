using ErrorOr;
using LibraryManagement.Domain.Common.Interface;
using LibraryManagement.Domain.PatronAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Patrons.List
{
	public record ListPatronRecord(int Id,
		string Name,
		string Email,
		string PhoneNumber, string Street, string City, string State,
		int BorrowRecordCount, int ReturnRecordCount,
		int PatronTypeId, string PatronTypeName);

	public record ListPatronDto(
		List<ListPatronRecord> Records,
		int TotalNumberOfPatrons);
	public record ListPatronQuery(int page, int pageSize,
		int SearchId = 0, string SearchPatronName = "",
		string SearchEmail = "") : IRequest<ErrorOr<ListPatronDto>>;
}
