using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Virgin.GenericRepository.DAL.Interfaces
{
    public interface IAppDbContext
    {
        bool IsDetached<TEntity>(TEntity entity) where TEntity : class;

        void SetEntityStateModified<TEntity>(TEntity entityToModifie) where TEntity : class;

        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}