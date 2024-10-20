using LibraryManagement.Application.Common.Interface;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Infastructure.Services
{
	public class EmailConfiguration
	{
		public string From { get; set; }
		public string SmtpServer { get; set; }
		public int Port { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }
	}

	public class EmailService : IEmailService
	{
		private readonly EmailConfiguration _emailConfig;
		private readonly ILogger<EmailService> _logger;

		public EmailService(EmailConfiguration emailConfiguration, ILogger<EmailService> logger)
		{
			_emailConfig = emailConfiguration;
			_logger = logger;
		}

		public async Task SendEmailAsync(Message message)
		{
			var emailMessage = CreateEmailMessage(message);
			await SendAsync(emailMessage);
		}

		private MimeMessage CreateEmailMessage(Message message)
		{
			var emailMessage = new MimeMessage();
			emailMessage.From.Add(new MailboxAddress(_emailConfig.UserName, _emailConfig.From));
			emailMessage.To.AddRange(message.To);
			emailMessage.Subject = message.Subject;
			emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };

			return emailMessage;
		}

		private async Task SendAsync(MimeMessage mailMessage)
		{
			using (var client = new SmtpClient())
			{
				try
				{
					client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, true);
					client.AuthenticationMechanisms.Remove("XOAUTH2");
					client.Authenticate(_emailConfig.UserName, _emailConfig.Password);

					await client.SendAsync(mailMessage);
				}
				catch(Exception e)
				{
					_logger.LogError(e.Message);
					//log an error message or throw an exception or both.
					throw;
				}
				finally
				{
					client.Disconnect(true);
					client.Dispose();
				}
			}
		}
	}
}
