using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using Virgin.Core.Helpers;
using Virgin.Core.Models;
using Virgin.Core.Services.Interfaces;
using Virgin.MSGraph.Services.Interfaces;
using Virgin.Shared.Exceptions;

namespace Virgin.Core.Services
{
    public class CustomerQRService : ICustomerQRService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CustomerQRService(ICustomerRepository customerRepository, IHttpContextAccessor httpContextAccessor)
        {
            _customerRepository = customerRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<QRDetails> CreateCustomerQRAsync(Customer customer)
        {
            var mailExists = await _customerRepository.EmailExistsAsync(customer.Email).ConfigureAwait(false);
            if (mailExists)
                throw new Exception(ExceptionMessages.CustomerEmailExists, new DoNotLogException());

            var qrDetails = await _customerRepository.CreateCustomerAsync(Customer.ToGraph(customer)).ConfigureAwait(false);

            var result = QRDetails.ToModel(qrDetails);
            result.QRAddress = QrAddressGenerator.Generate(_httpContextAccessor, result.Customer.Id);

            //TODO: automatizirat qr https://github.com/MKaranusic/QrCodeGenerator

            return result;
        }
    }
}