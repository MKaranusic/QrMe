using Virgin.GenericRepository.DAL;
using Virgin.Infrastructure.DAL.Interfaces;
using Virgin.Infrastructure.Repositories;
using Virgin.Infrastructure.Repositories.Interfaces;

namespace Virgin.Infrastructure.DAL
{
    public class VirginUnitOfWork : AppUnitOfWork, IVirginUnitOfWork
    {
        private ICustomerRedirectRepository _customerRedirectRepository;
        public VirginUnitOfWork(VirginDbContext context) : base(context)
        {
        }
        public ICustomerRedirectRepository CustomerRedirectRepository => _customerRedirectRepository ??= new CustomerRedirectRepository(_context);
    }
}