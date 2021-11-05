using System.Collections.Generic;
using System.Threading.Tasks;
using Virgin.Core.Models;

namespace Virgin.Core.Services.Interfaces
{
    public interface ICustomerRedirectService
    {
        Task<CustomerRedirect> GetCustomerRedirectByIDAsync(int id);

        Task<List<CustomerRedirect>> GetCustomerRedirectsAsync();

        Task<List<CustomerRedirect>> GetCustomerRedirectsAsync(string customerId);

        Task<int> CreateCustomerRedirectAsync(CustomerRedirect customerRedirect);

        Task<int> UpdateCustomerRedirectAsync(CustomerRedirect customerRedirect);

        Task<int> DeleteCustomerRedirectAsync(int id);

        Task<int> DeleteCustomerRedirectNoCheckAsync(int id);

        Task IncrementActiveCustomerUrlTimesViewedAsync(string customerId);

        Task<int> GetCustomerTimesViewedSumAsync();
    }
}