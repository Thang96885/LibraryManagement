using LibraryManagement.Domain.Common.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Domain.Common.Interface
{
	public interface IBaseRepository<TEntity>
		where TEntity : Entity
	{
		TEntity? Find(Guid id);
		Task<TEntity>? FindAsync(Guid id);
		void Add(TEntity entity);
		void Update(TEntity entity);
		void Delete(TEntity entity);

		List<TEntity> List();
		Task<List<TEntity>> ListAsync();
		int SaveChange();
		Task<int> SaveChangeAsync();
	}
}
