using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Domain.BookAggregate.Services
{
	public class BookCopyService
	{
		public bool CheckIBNS(string ibsn)
		{
			return (ibsn.Length == 13 && ibsn.All(char.IsDigit)) || (ibsn.Length == 10 && ibsn.All(c => char.IsLetterOrDigit(c)));
		}

	}
}
