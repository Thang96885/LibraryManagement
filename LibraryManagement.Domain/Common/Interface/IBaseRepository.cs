using LibraryManagement.Domain.Common.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Domain.Common.Interface
{
	public interface IBaseRepository<T, TId, TIdType>
		where TId : AggregateRootId<TIdType>
		where T : AggregateRoot<TId, TIdType>
	{
		void Find(TId id);
		Task FindAsync(TId id);
		void Add(T entity);
		void Update(T entity);

		void List();
		Task ListAsync();
		int SaveChange();
		Task<int> SaveChangeAsync();
	}
}
