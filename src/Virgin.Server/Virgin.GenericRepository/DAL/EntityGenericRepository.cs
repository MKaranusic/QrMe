using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Virgin.GenericRepository.DAL.Interfaces;
using Virgin.GenericRepository.Helpers;
using Virgin.GenericRepository.Models;

namespace Virgin.GenericRepository.DAL
{
    public class EntityGenericRepository<TEntity, TKey> : GenericRepository<TEntity, TKey>, IGenericRepository<TEntity, TKey> where TEntity : EntityBase<TKey>
    {
        public EntityGenericRepository(IAppDbContext context) : base(context)
        {
        }

        #region IGenericRepository

        public override void Delete(TKey id)
        {
            var item = DbSet.Find(id);
            item.IsDeleted = true;
            Update(item);
        }

        public override void Delete(TEntity entityToDelete)
        {
            if (IsDetached(entityToDelete))
                DbSet.Attach(entityToDelete);

            entityToDelete.IsDeleted = true;

            Update(entityToDelete);
        }

        public override void Update(TEntity entityToUpdate)
        {
            entityToUpdate.ModifiedUtc = DateTime.UtcNow;
            base.Update(entityToUpdate);
        }

        public override void Insert(TEntity entity)
        {
            entity.CreatedUtc = DateTime.UtcNow;
            entity.ModifiedUtc = DateTime.UtcNow;
            base.Insert(entity);
        }

        public override Task<TEntity> GetByIDAsync(TKey id)
        {
            return base.GetByIDAsync(id);
        }

        public override Task<List<TModel>> GetModelAsync<TModel>(Expression<Func<TEntity, TModel>> select,
                                                                 Expression<Func<TEntity, bool>> filter = null,
                                                                 Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                                 int skip = 0, int take = 0)
        {
            return base.GetModelAsync(select, FilterDeleted(filter), orderBy, skip, take);
        }

        public override Task<TModel> GetModelFirstOrDefaultAsync<TModel>(Expression<Func<TEntity, TModel>> select,
                                                                         Expression<Func<TEntity, bool>> filter = null,
                                                                         Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            return base.GetModelFirstOrDefaultAsync(select, FilterDeleted(filter), orderBy);
        }

        public override Task<TModel> GetModelSingleOrDefaultAsync<TModel>(Expression<Func<TEntity, TModel>> select,
                                                                          Expression<Func<TEntity, bool>> filter = null,
                                                                          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            return base.GetModelSingleOrDefaultAsync(select, FilterDeleted(filter), orderBy);
        }

        public override Task<int> GetCountAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            return base.GetCountAsync(FilterDeleted(filter));
        }

        public override Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            return base.ExistsAsync(FilterDeleted(filter));
        }

        #endregion IGenericRepository

        protected virtual Expression<Func<TEntity, bool>> FilterDeleted(Expression<Func<TEntity, bool>> filter)
        {
            var predicate = PredicateExtensions.Begin<TEntity>();
            predicate = predicate.And(x => !x.IsDeleted);

            if (filter != null)
                predicate = predicate.And(filter);

            return predicate;
        }
    }
}