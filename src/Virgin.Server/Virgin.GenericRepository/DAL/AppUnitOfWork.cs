using System.Threading.Tasks;
using Virgin.GenericRepository.DAL.Interfaces;

namespace Virgin.GenericRepository.DAL
{
    public class AppUnitOfWork : IAppUnitOfWork
    {
        protected readonly IAppDbContext _context;

        public AppUnitOfWork(IAppDbContext context)
        {
            _context = context;
        }

        public Task<int> SaveAsync() => _context.SaveChangesAsync();
    }
}