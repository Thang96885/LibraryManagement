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
		List<TEntity> List(int page, int pageSize);
		Task<List<TEntity>> ListAsync();
		Task<List<TEntity>> ListAsync(int page, int pageSize);
		int SaveChange();
		Task<int> SaveChangeAsync();
	}
}
