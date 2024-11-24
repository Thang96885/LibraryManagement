using ErrorOr;
using LibraryManagement.Domain.Common.Interface;
using LibraryManagement.Domain.PatronAggregate;
using LibraryManagement.Domain.PatronAggregate.ValueObjects;
using MapsterMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using LibraryManagement.Application.Common.Interface;

namespace LibraryManagement.Application.Patrons.Create
{
    public class CreatePatronCommandHandler : IRequestHandler<CreatePatronCommand, ErrorOr<CreatePatronResult>>
    {
        private readonly IBaseRepository<Patron> _patronRepository;
        private readonly IMapper _mapper;
        private readonly IIdentityService _identityService;

		public CreatePatronCommandHandler(IBaseRepository<Patron> repository, IMapper mapper, IIdentityService identityService)
		{
			_patronRepository = repository;
			_mapper = mapper;
			_identityService = identityService;
		}

		public async Task<ErrorOr<CreatePatronResult>> Handle(CreatePatronCommand request, CancellationToken cancellationToken)
        {

            var patron = Patron.Create(request.Name, request.Email, 
	            request.PhoneNumber, 
	            PatronAddress.Create(request.Address.Street, 
		            request.Address.City, request.Address.State, 
		            request.Address.ZipCode),
	            PatronPatronTypeId.Create(request.PatronTypeId));

            _patronRepository.Add(patron);
            

            await _patronRepository.SaveChangeAsync();
            await Task.Delay(10);
            var userName = RemoveDiacritics(patron.Name).Replace(" ", string.Empty) + patron.Id.ToString();
            await Task.Delay(100); 
            await _identityService.SignInAsync(new RegisterInfo(patron, userName, "Abc@123"));

            return new CreatePatronResult(patron.Id);
        }
		
        private static string RemoveDiacritics(string input)
        {
	        if (string.IsNullOrEmpty(input))
		        return input;

	        // Normalize chuỗi thành dạng FormD (Decomposition Form)
	        input = input.Replace('Đ', 'D');
	        input = input.Replace('đ', 'd');
	        
	        string normalizedString = input.Normalize(NormalizationForm.FormD);
	        StringBuilder stringBuilder = new StringBuilder();

	        foreach (char c in normalizedString)
	        {
		        // Lấy thông tin Unicode của ký tự
		        UnicodeCategory unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);

		        // Chỉ giữ lại các ký tự không phải ký tự dấu (non-spacing mark)
		        if (unicodeCategory != UnicodeCategory.NonSpacingMark)
		        {
			        stringBuilder.Append(c);
		        }
	        }

	        // Normalize lại chuỗi thành FormC (Composed Form)
	        return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}
