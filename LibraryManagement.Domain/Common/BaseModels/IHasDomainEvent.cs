using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Domain.Common.BaseModels
{
	public interface IHasDomainEvent
	{
		public IReadOnlyList<IDomainEvent> DomainEvents { get; }

		public void ClearDomainEvents();

		public void AddDomainEvent(IDomainEvent domainEvent);
	}
}
