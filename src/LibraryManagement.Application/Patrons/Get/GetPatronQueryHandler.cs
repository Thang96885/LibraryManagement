using ErrorOr;
using LibraryManagement.Domain.Common.Interface;
using LibraryManagement.Domain.PatronAggregate;
using MapsterMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Patrons.Get
{
	public class GetPatronQueryHandler : IRequestHandler<GetPatronQuery, ErrorOr<GetPatronDto>>
	{
		private readonly IBaseRepository<Patron> _patronRepository;
		private readonly IMapper _mapper;

		public GetPatronQueryHandler(IBaseRepository<Patron> patronRepository, IMapper mapper)
		{
			_patronRepository = patronRepository;
			_mapper = mapper;
		}

		public async Task<ErrorOr<GetPatronDto>> Handle(GetPatronQuery request, CancellationToken cancellationToken)
		{
			var patron = await _patronRepository.FindAsync(request.id);

			if(patron == null)
			{
				return Error.NotFound("Patron id not found");
			}

			return _mapper.Map<GetPatronDto>(patron);
		}
	}
}
