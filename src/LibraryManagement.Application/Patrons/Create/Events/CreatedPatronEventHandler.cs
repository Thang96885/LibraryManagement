using LibraryManagement.Application.Common.Interface;
using LibraryManagement.Domain.PatronAggregate.Events;
using MediatR;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Patrons.Create.Events
{
	public class CreatedPatronEventHandler : INotificationHandler<CreatedPatron>
	{
		private readonly IEmailService _emailService;

		public CreatedPatronEventHandler(IEmailService emailService)
		{
			_emailService = emailService;
		}

		public async Task Handle(CreatedPatron notification, CancellationToken cancellationToken)
		{
			var message = new Message(
				new List<MailboxAddress> { new(notification.Patron.Name, notification.Patron.Email) },
				"Welcome to the Library",
				"Welcome to the Library, we are glad to have you as our patron"
			);

			await _emailService.SendEmailAsync(message);
		}
	}
}
