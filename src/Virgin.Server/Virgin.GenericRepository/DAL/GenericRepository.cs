using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Virgin.GenericRepository.DAL.Interfaces;

namespace Virgin.GenericRepository.DAL
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : class
    {
        protected readonly IAppDbContext _context;
        protected readonly DbSet<TEntity> DbSet;

        public GenericRepository(IAppDbContext context)
        {
            _context = context;
            DbSet = _context.Set<TEntity>();
        }

        #region IGenericRepository

        public virtual void Delete(TKey id)
        {
            var item = DbSet.Find(id);
            Delete(item);
        }

        public virtual void Delete(TEntity entity)
        {
            if (IsDetached(entity))
                DbSet.Attach(entity);

            DbSet.Remove(entity);
        }

        public virtual void Update(TEntity entity)
        {
            if (IsDetached(entity))
                DbSet.Attach(entity);

            SetEntityStateModified(entity);
        }

        public virtual void Insert(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public virtual Task<TEntity> GetByIDAsync(TKey id)
        {
            return DbSet.FindAsync(id).AsTask();
        }

        public virtual Task<List<TModel>> GetModelAsync<TModel>(Expression<Func<TEntity, TModel>> select,
                                                                Expression<Func<TEntity, bool>> filter = null,
                                                                Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                                int skip = 0, int take = 0)
        {
            var query = GetModelQuery(select, filter, orderBy, skip, take);

            return query.ToListAsync();
        }

        public virtual Task<TModel> GetModelFirstOrDefaultAsync<TModel>(Expression<Func<TEntity, TModel>> select,
                                                                Expression<Func<TEntity, bool>> filter = null,
                                                                Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            var query = GetModelQuery(select, filter, orderBy, 0, 0);

            return query.FirstOrDefaultAsync();
        }

        public virtual Task<TModel> GetModelSingleOrDefaultAsync<TModel>(Expression<Func<TEntity, TModel>> select,
                                                                 Expression<Func<TEntity, bool>> filter = null,
                                                                 Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            var query = GetModelQuery(select, filter, orderBy, 0, 0);

            return query.SingleOrDefaultAsync();
        }

        public virtual Task<int> GetCountAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            if (filter != null)
                return DbSet.CountAsync(filter);

            return DbSet.CountAsync();
        }

        public virtual Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            if (filter != null)
                return DbSet.AnyAsync(filter);

            return DbSet.AnyAsync();
        }

        #endregion IGenericRepository

        protected IQueryable<TModel> GetModelQuery<TModel>(Expression<Func<TEntity, TModel>> select,
                                                         Expression<Func<TEntity, bool>> filter,
                                                         Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
                                                         int skip,
                                                         int take)
        {
            IQueryable<TEntity> query = DbSet;

            if (filter != null)
                query = query.Where(filter);

            query = orderBy != null ? orderBy(query) : query;

            if (take > 0)
            {
                query = skip > 0 ? query.Skip(skip) : query;

                query = query.Take(take);
            }

            return query.Select(select);
        }

        #region Entity state management

        protected bool IsDetached(TEntity entityToCheck)
        {
            return _context.IsDetached(entityToCheck);
        }

        protected void SetEntityStateModified(TEntity entityToModifie)
        {
            _context.SetEntityStateModified(entityToModifie);
        }

        #endregion Entity state management
    }
}