using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Domain.Common.Enums
{
	public enum BookStatus
	{
		NotChange,
		Available,
		Borrowed,
		UnderMaintenance,
		Lost
	}
}
