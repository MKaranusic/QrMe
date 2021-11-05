using System.Threading.Tasks;
using Virgin.Core.Models;

namespace Virgin.Core.Services.Interfaces
{
    public interface ICustomerQRService
    {
        Task<QRDetails> CreateCustomerQRAsync(Customer customer);
    }
}