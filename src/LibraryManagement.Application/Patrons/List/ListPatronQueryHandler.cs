using ErrorOr;
using LibraryManagement.Domain.Common.Interface;
using LibraryManagement.Domain.PatronAggregate;
using Mapster;
using MapsterMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagement.Domain.PatronTypeAggregate;

namespace LibraryManagement.Application.Patrons.List
{
	public class ListPatronQueryHandler : IRequestHandler<ListPatronQuery, ErrorOr<ListPatronDto>>
	{
		private readonly IBaseRepository<Patron> _patronRepository;
		private readonly IBaseRepository<PatronType> _typeRepository;

		public ListPatronQueryHandler(IBaseRepository<Patron> patronRepository, IBaseRepository<PatronType> typeRepository)
		{
			_patronRepository = patronRepository;
			_typeRepository = typeRepository;
		}

		public async Task<ErrorOr<ListPatronDto>> Handle(ListPatronQuery request, CancellationToken cancellationToken)
		{
			var patrons = new List<Patron>();
			int totalNumberOfPatrons = 0;

			if (request.SearchId == 0 && request.SearchPatronName == "" && request.SearchEmail == "")
			{
				totalNumberOfPatrons = _patronRepository.GetNumberOfEntities();
				patrons = await _patronRepository.ListAsync(request.page, request.pageSize);
			}
			else
			{
				var patronQuery = _patronRepository.GetQueryable();
				
				if(request.SearchId != 0)
					patronQuery = patronQuery.Where(p => p.Id == request.SearchId);
				if(request.SearchPatronName != "")
					patronQuery = patronQuery.Where(p => p.Name.Contains(request.SearchPatronName));
				if(request.SearchEmail != "")
					patronQuery = patronQuery.Where(p => p.Email.Contains(request.SearchEmail));
				
				totalNumberOfPatrons = patronQuery.Count();
				patrons = patronQuery.Skip((request.page -1 ) * request.pageSize ).Take(request.pageSize).ToList();
			}

			var patronRecords = new List<ListPatronRecord>();

			foreach (var patron in patrons)
			{
				var patronType = _typeRepository.Find(patron.TypeId.Value);
				var patronRecord = new ListPatronRecord(
					patron.Id, patron.Name, patron.Email, patron.PhoneNumber, patron.Address.Street,
					patron.Address.City, patron.Address.State, patron.BorrowRecordIds.Count,
					patron.ReturnRecordIds.Count, patronType.Id, patronType.Name);
				patronRecords.Add(patronRecord);
			}

			return new ListPatronDto(patronRecords, totalNumberOfPatrons);
		}
	}
}
