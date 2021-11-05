using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Virgin.Core.Models;
using Virgin.Core.Services.Interfaces;
using Virgin.Infrastructure.DAL.Interfaces;
using Virgin.Shared.Exceptions;

namespace Virgin.Core.Services
{
    public class CustomerRedirectService : ICustomerRedirectService
    {
        private readonly IVirginUnitOfWork _unitOfWork;
        private readonly IUserContextService _userContextService;

        public CustomerRedirectService(IVirginUnitOfWork unitOfWork, IUserContextService userContextService)
        {
            _unitOfWork = unitOfWork;
            _userContextService = userContextService;
        }

        public Task<CustomerRedirect> GetCustomerRedirectByIDAsync(int id) =>
            _unitOfWork.CustomerRedirectRepository.GetModelSingleOrDefaultAsync(CustomerRedirect.Select, x => x.CustomerId == _userContextService.CustomerId && x.Id == id);

        public Task<List<CustomerRedirect>> GetCustomerRedirectsAsync() =>
            _unitOfWork.CustomerRedirectRepository.GetModelAsync(
                CustomerRedirect.Select,
                x => x.CustomerId == _userContextService.CustomerId,
                x => x.OrderByDescending(y => y.ModifiedUtc),
                0, CustomerRedirecConfig.Take
                );

        public Task<List<CustomerRedirect>> GetCustomerRedirectsAsync(string customerId) =>
            _unitOfWork.CustomerRedirectRepository.GetModelAsync(
                CustomerRedirect.Select,
                x => x.CustomerId == customerId,
                x => x.OrderByDescending(y => y.ModifiedUtc),
                0, CustomerRedirecConfig.Take
                );

        public async Task<int> CreateCustomerRedirectAsync(CustomerRedirect customerRedirect)
        {
            customerRedirect.CustomerId = _userContextService.CustomerId;

            if (customerRedirect.IsActive)
                await _unitOfWork.CustomerRedirectRepository.DeactivateCustomerUrlsAsync(customerRedirect.CustomerId).ConfigureAwait(false);

            var customerRedirectEntity = CustomerRedirect.ToEntity(customerRedirect);

            _unitOfWork.CustomerRedirectRepository.Insert(customerRedirectEntity);

            await _unitOfWork.SaveAsync().ConfigureAwait(false);

            return customerRedirectEntity.Id;
        }

        public async Task<int> UpdateCustomerRedirectAsync(CustomerRedirect customerRedirect)
        {
            var customerRedirectToUpdate = await GetCustomerRedirectByIDAsync(customerRedirect.Id).ConfigureAwait(false);

            if (customerRedirectToUpdate is null)
                throw new Exception(ExceptionMessages.CustomerRedirectNotFound, new DoNotLogException());

            customerRedirectToUpdate.IsActive = customerRedirect.IsActive;
            customerRedirectToUpdate.TargetUrl = customerRedirect.TargetUrl;
            customerRedirectToUpdate.Name = customerRedirect.Name;

            if (customerRedirectToUpdate.IsActive)
                await _unitOfWork.CustomerRedirectRepository.DeactivateCustomerUrlsAsync(customerRedirectToUpdate.CustomerId, customerRedirectToUpdate.Id).ConfigureAwait(false);

            await _unitOfWork.SaveAsync().ConfigureAwait(false);

            _unitOfWork.CustomerRedirectRepository.Update(CustomerRedirect.ToEntity(customerRedirectToUpdate));

            await _unitOfWork.SaveAsync().ConfigureAwait(false);

            return customerRedirectToUpdate.Id;
        }

        public async Task<int> DeleteCustomerRedirectAsync(int id)
        {
            var customerRedirectToDelete = await GetCustomerRedirectByIDAsync(id).ConfigureAwait(false);

            if (customerRedirectToDelete is null)
                throw new Exception(ExceptionMessages.CustomerRedirectNotFound, new DoNotLogException());

            _unitOfWork.CustomerRedirectRepository.Delete(customerRedirectToDelete.Id);

            await _unitOfWork.SaveAsync().ConfigureAwait(false);

            return customerRedirectToDelete.Id;
        }

        public async Task<int> DeleteCustomerRedirectNoCheckAsync(int id)
        {
            _unitOfWork.CustomerRedirectRepository.Delete(id);

            await _unitOfWork.SaveAsync().ConfigureAwait(false);

            return id;
        }

        public async Task IncrementActiveCustomerUrlTimesViewedAsync(string customerId)
        {
            var activeCustomerRedirect = await _unitOfWork.CustomerRedirectRepository.GetModelSingleOrDefaultAsync(CustomerRedirect.Select, x => x.CustomerId == customerId && x.IsActive == true);

            activeCustomerRedirect.TimesViewed++;

            _unitOfWork.CustomerRedirectRepository.UpdateBase(CustomerRedirect.ToEntity(activeCustomerRedirect));
            await _unitOfWork.SaveAsync().ConfigureAwait(false);
        }

        public async Task<int> GetCustomerTimesViewedSumAsync()
        {
            var customerRedirects = await GetCustomerRedirectsAsync().ConfigureAwait(false);

            var result = customerRedirects.Sum(x => x.TimesViewed);
            return result;
        }
    }
}