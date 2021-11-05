using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Virgin.GenericRepository.DAL.Interfaces
{
    public interface IGenericRepository<TEntity, TKey> where TEntity : class
    {
        void Delete(TKey id);

        void Delete(TEntity entity);

        void Update(TEntity entityToUpdate);

        void Insert(TEntity entity);

        Task<TEntity> GetByIDAsync(TKey id);

        Task<List<TModel>> GetModelAsync<TModel>(Expression<Func<TEntity, TModel>> select, Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, int skip = 0, int take = 0);

        Task<TModel> GetModelFirstOrDefaultAsync<TModel>(Expression<Func<TEntity, TModel>> select, Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null);

        Task<TModel> GetModelSingleOrDefaultAsync<TModel>(Expression<Func<TEntity, TModel>> select, Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null);

        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> filter = null);

        Task<int> GetCountAsync(Expression<Func<TEntity, bool>> filter = null);
    }
}