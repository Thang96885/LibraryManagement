using LibraryManagement.Domain.BookAggregate.Services;
using LibraryManagement.Domain.Common.BaseModels;
using LibraryManagement.Domain.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Domain.BookAggregate.Entities
{
	public class BookCopy : Entity<string>
	{
		private static readonly BookCopyService _bookCopyService = new BookCopyService();
		public DateTime AcquisitionDate { get; private set; }
		public BookStatus Status { get; private set; } = BookStatus.Available;
		public decimal Price { get; private set; }

		public BookPhysicalCondition PhysicalCondition { get; private set; } = BookPhysicalCondition.Good;

		private BookCopy(string Id, DateTime AcquisitionDate, decimal price)
		{
			this.Id = Id;
			this.AcquisitionDate = AcquisitionDate;
			Price = price;
		}
		
		public static BookCopy Create(string Id, DateTime accquisitionDate, decimal price)
		{
			if (!_bookCopyService.CheckIBNS(Id))
			{
				throw new ArgumentException("Invalid IBNS");
			}
			return new(Id, accquisitionDate, price);
		}

		public void ChangeStatus(BookStatus status)
		{
			this.Status = status;
		}

		public void UpdateBookValue(BookStatus status,
			BookPhysicalCondition condition,
			decimal price)
		{
			if(status != BookStatus.NotChange)
				this.Status = status;
			if(condition != BookPhysicalCondition.NotChange)
				this.PhysicalCondition = condition;
			this.Price = price;
		}
	}
}
