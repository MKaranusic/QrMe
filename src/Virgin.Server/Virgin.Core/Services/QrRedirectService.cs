using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using Virgin.Core.Configuration;
using Virgin.Core.Services.Interfaces;
using Virgin.Infrastructure.DAL.Interfaces;
using Virgin.MSGraph.Services.Interfaces;

namespace Virgin.Core.Services
{
    public class QrRedirectService : IQrRedirectService
    {
        private readonly IVirginUnitOfWork _unitOfWork;
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerRedirectService _customerRedirectService;
        private readonly IOptions<ClientConfiguration> _options;

        public QrRedirectService(IVirginUnitOfWork unitOfWork, ICustomerRepository customerRepository, ICustomerRedirectService customerRedirectService, IOptions<ClientConfiguration> options)
        {
            _unitOfWork = unitOfWork;
            _customerRepository = customerRepository;
            _customerRedirectService = customerRedirectService;
            _options = options;
        }

        public async Task<Uri> GetRedirectUriAsync(string customerId)
        {
            var customerExists = await _customerRepository.UserExistsAsync(customerId).ConfigureAwait(false);

            if (!customerExists)
                throw new Exception(ExceptionMessages.QrCodeNotFound);

            var tragetUrl = await _unitOfWork.CustomerRedirectRepository.GetActiveCustomerUrlAsync(customerId).ConfigureAwait(false);
            if (tragetUrl is not null)
                await _customerRedirectService.IncrementActiveCustomerUrlTimesViewedAsync(customerId).ConfigureAwait(false);
            else
                tragetUrl = _options.Value.Url + "/not-set-url";

            return new Uri(tragetUrl);
        }
    }
}