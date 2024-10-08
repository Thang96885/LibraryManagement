using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Domain.Common.Interface
{
	public interface IDateTimeProvider
	{
		DateTime Now { get; }
	}
}
