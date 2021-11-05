using Virgin.GenericRepository.DAL.Interfaces;
using Virgin.Infrastructure.Repositories.Interfaces;

namespace Virgin.Infrastructure.DAL.Interfaces
{
    public interface IVirginUnitOfWork : IAppUnitOfWork
    {
        ICustomerRedirectRepository CustomerRedirectRepository { get; }
    }
}