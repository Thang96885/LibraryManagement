using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Domain.Common.BaseModels
{
	public abstract class AggregateRoot<TId, TIdType> : Entity<TId>
		where TId : AggregateRootId<TIdType>
	{
		public new AggregateRootId<TIdType> Id { get; protected set; }

	}
}
