using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Domain.Common.Enums
{
	public enum BookPhysicalCondition
	{
		New,           // Mới
		Good,          // Tốt
		Fair,          // Khá
		Poor,          // Kém
		Damaged,       // Hư hỏng
		NeedsRepair    // Cần sửa chữa
	}
}
