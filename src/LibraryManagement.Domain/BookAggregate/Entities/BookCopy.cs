using LibraryManagement.Domain.Common.BaseModels;
using LibraryManagement.Domain.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Domain.BookAggregate.Entities
{
	// Id of BookCopy is IBNS code
	public class BookCopy : Entity<string>
	{
		public DateTime AcquisitionDate { get; private set; }
		public BookStatus Status { get; private set; } = BookStatus.Available;

		public BookPhysicalCondition PhysicalCondition { get; private set; } = BookPhysicalCondition.Good;

		private BookCopy(string Id, DateTime AcquisitionDate)
		{
			this.Id = Id;
			this.AcquisitionDate = AcquisitionDate;
		}
		
		public BookCopy Create(string Id, DateTime accquisitionDate)
		{
			return new(Id, AcquisitionDate);
		}
	}
}
