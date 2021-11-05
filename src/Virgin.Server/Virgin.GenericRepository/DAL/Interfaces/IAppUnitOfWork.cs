using System.Threading.Tasks;

namespace Virgin.GenericRepository.DAL.Interfaces
{
    public interface IAppUnitOfWork
    {
        public Task<int> SaveAsync();
    }
}