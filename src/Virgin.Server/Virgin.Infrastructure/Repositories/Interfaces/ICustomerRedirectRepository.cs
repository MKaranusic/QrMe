using System.Threading.Tasks;
using Virgin.GenericRepository.DAL.Interfaces;
using Virgin.Infrastructure.Entities;

namespace Virgin.Infrastructure.Repositories.Interfaces
{
    public interface ICustomerRedirectRepository : IGenericRepository<CustomerRedirect, int>
    {
        Task DeactivateCustomerUrlsAsync(string customerId, int id = 0);

        Task<string> GetActiveCustomerUrlAsync(string customerId);

        int UpdateBase(CustomerRedirect entity);
    }
}