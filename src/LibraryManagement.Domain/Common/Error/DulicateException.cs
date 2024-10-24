using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Domain.Common.Error
{
	public class DulicateException : Exception
	{

		public DulicateException(string message) : base(message)
		{

		}
	}
}
