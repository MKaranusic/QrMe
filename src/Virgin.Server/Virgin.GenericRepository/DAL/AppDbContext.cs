using Microsoft.EntityFrameworkCore;
using Virgin.GenericRepository.DAL.Interfaces;

namespace Virgin.GenericRepository.DAL
{
    public abstract class AppDbContext : DbContext, IAppDbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
            this.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public bool IsDetached<TEntity>(TEntity entity) where TEntity : class
        {
            return Entry(entity).State == EntityState.Detached;
        }

        public void SetEntityStateModified<TEntity>(TEntity entityToModifie) where TEntity : class
        {
            Entry(entityToModifie).State = EntityState.Modified;
        }
    }
}