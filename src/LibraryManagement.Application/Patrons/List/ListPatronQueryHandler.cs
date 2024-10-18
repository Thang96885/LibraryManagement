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

namespace LibraryManagement.Application.Patrons.List
{
	public class ListPatronQueryHandler : IRequestHandler<ListPatronQuery, ErrorOr<List<ListPatronDto>>>
	{
		private readonly IBaseRepository<Patron> _patronRepository;
		private readonly IMapper _mapper;

		public ListPatronQueryHandler(IBaseRepository<Patron> patronRepository, IMapper mapper)
		{
			_patronRepository = patronRepository;
			_mapper = mapper;
		}

		public async Task<ErrorOr<List<ListPatronDto>>> Handle(ListPatronQuery request, CancellationToken cancellationToken)
		{
			var patrons = await _patronRepository.ListAsync(request.page, request.pageSize);

			var result = new List<ListPatronDto>();
			foreach(var patron in patrons)
			{
				result.Add(_mapper.Map<ListPatronDto>(patron));
			}
			return result;
		}
	}
}
