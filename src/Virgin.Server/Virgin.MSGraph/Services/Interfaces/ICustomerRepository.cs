using System.Threading.Tasks;
using Virgin.MSGraph.Models;

namespace Virgin.MSGraph.Services.Interfaces
{
    public interface ICustomerRepository
    {
        Task<QRDetails> CreateCustomerAsync(Customer customer);

        Task<bool> EmailExistsAsync(string email);

        Task<bool> UserExistsAsync(string userId);
    }
}