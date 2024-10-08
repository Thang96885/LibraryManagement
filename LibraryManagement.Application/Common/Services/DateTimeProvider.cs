using LibraryManagement.Domain.Common.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Common.Services
{
	public class DateTimeProvider : IDateTimeProvider
	{
		public DateTime Now => DateTime.Now;
	}
}
