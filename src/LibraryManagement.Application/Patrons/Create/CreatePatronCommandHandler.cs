using ErrorOr;
using LibraryManagement.Domain.Common.Interface;
using LibraryManagement.Domain.PatronAggregate;
using LibraryManagement.Domain.PatronAggregate.ValueObjects;
using MapsterMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Patrons.Create
{
    public class CreatePatronCommandHandler : IRequestHandler<CreatePatronCommand, ErrorOr<CreatePatronResult>>
    {
        private readonly IBaseRepository<Patron> _patronRepository;
        private readonly IMapper _mapper;

		public CreatePatronCommandHandler(IBaseRepository<Patron> repository, IMapper mapper)
		{
			_patronRepository = repository;
			_mapper = mapper;
		}

		public async Task<ErrorOr<CreatePatronResult>> Handle(CreatePatronCommand request, CancellationToken cancellationToken)
        {

            var patron = Patron.Create(request.Name, request.Email, request.PhoneNumber, PatronAddress.Create(request.Address.Street, request.Address.City, request.Address.State, request.Address.ZipCode));

            _patronRepository.Add(patron);

            await _patronRepository.SaveChangeAsync();

            return new CreatePatronResult(patron.Id);
        }
    }
}
