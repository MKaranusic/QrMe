using System;
using System.Threading.Tasks;

namespace Virgin.Core.Services.Interfaces
{
    public interface IQrRedirectService
    {
        Task<Uri> GetRedirectUriAsync(string customerId);
    }
}