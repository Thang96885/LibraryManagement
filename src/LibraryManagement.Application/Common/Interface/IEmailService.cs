using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Common.Interface
{

    public record Message(
		List<MailboxAddress> To,
		string Subject,
		string Content);

    public interface IEmailService
	{
		Task SendEmailAsync(Message message);

	}
}
