using LibraryManagement.Domain.Common.BaseModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Domain.Common.Interface
{
	public interface IBaseRepository<TEntity>
		where TEntity : Entity
	{
		TEntity? Find(int id);
		Task<TEntity>? FindAsync(int id);
		void Add(TEntity entity);
		void Update(TEntity entity);
		void Delete(TEntity entity);

		List<TEntity> List();
		List<TEntity> List(int page, int pageSize);
		Task<List<TEntity>> ListAsync();
		Task<List<TEntity>> ListAsync(int page, int pageSize);
		
		IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
		IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, int page, int pageSize);
		Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
		Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, int page, int pageSize);
		int SaveChange();
		Task<int> SaveChangeAsync();
	}
}
