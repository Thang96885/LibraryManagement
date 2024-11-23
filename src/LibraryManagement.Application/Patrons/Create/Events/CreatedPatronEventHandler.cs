using LibraryManagement.Application.Common.Interface;
using LibraryManagement.Domain.PatronAggregate.Events;
using MediatR;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagement.Domain.Common.Interface;
using LibraryManagement.Domain.PatronTypeAggregate;

namespace LibraryManagement.Application.Patrons.Create.Events
{
	public class CreatedPatronEventHandler : INotificationHandler<CreatedPatron>
	{
		private readonly IBaseRepository<PatronType> _patronTypeRepository; 

		public CreatedPatronEventHandler(IEmailService emailService, IBaseRepository<PatronType> patronTypeRepository)
		{
			_patronTypeRepository = patronTypeRepository;
		}

		public async Task Handle(CreatedPatron notification, CancellationToken cancellationToken)
		{
			var patronType = await _patronTypeRepository.FindAsync(notification.Patron.TypeId.Value);
			
			if(patronType == null)
				throw new NullReferenceException();

			patronType.AddPatron(notification.Patron.Id);
			
			_patronTypeRepository.Update(patronType);
			await _patronTypeRepository.SaveChangeAsync();
		}
		
		
		
	}
}
