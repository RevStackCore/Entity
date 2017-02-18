using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using RevStackCore.Pattern;
using RevStackCore.Extensions;

namespace RevStackCore.Entity
{
	public class EntityRepository<TEntity,TKey> : IRepository<TEntity,TKey> where TEntity:class, IEntity<TKey>
	{
		private readonly DbContext _dbContext;
		public EntityRepository(DbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public virtual TEntity Add(TEntity entity)
		{
			_dbContext.Entry(entity).State = EntityState.Added;
			_dbContext.SaveChanges();
			return entity;
		}

		public virtual void Delete(TEntity entity)
		{
			_dbContext.Entry(entity).State = EntityState.Deleted;
			_dbContext.SaveChanges();
		}

		public virtual IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
		{
			return _dbContext.Set<TEntity>().Where(predicate).AsQueryable();
		}

		public virtual IEnumerable<TEntity> Get()
		{
			return _dbContext.Set<TEntity>();
		}

		public virtual TEntity GetById(TKey id)
		{
			return _dbContext.Set<TEntity>().Where(x => x.Compare(x.Id, id)).FirstOrDefault();
		}

		public virtual TEntity Update(TEntity entity)
		{
			_dbContext.Entry(entity).State = EntityState.Modified;
			_dbContext.SaveChanges();
			return entity;
		}
	}
}
