using Moq;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Virgin.Core.Models;
using Virgin.Core.Services;
using Virgin.Core.Services.Interfaces;
using Virgin.Infrastructure.DAL.Interfaces;
using Xunit;
using Entity = Virgin.Infrastructure.Entities;

namespace Virgin.Core.Test.Services
{
#pragma warning disable IDE0051

    public class CustomerRedirectServiceTests
    {
        private readonly Mock<IVirginUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IUserContextService> _userContextServiceMock;
        private readonly CustomerRedirectService _sut;

        public CustomerRedirectServiceTests()
        {
            _unitOfWorkMock = new();
            _userContextServiceMock = new();

            _sut = new(_unitOfWorkMock.Object, _userContextServiceMock.Object);
        }

        [Fact]
        private async Task GetCustomerRedirectForCustomerByIDAsync_VerifyCall()
        {
            int customerRedirectID = 1;
            _unitOfWorkMock.Setup(x => x.CustomerRedirectRepository.GetModelSingleOrDefaultAsync(
                It.IsAny<Expression<Func<Entity.CustomerRedirect, CustomerRedirect>>>(),
                It.IsAny<Expression<Func<Entity.CustomerRedirect, bool>>>(),
                null));

            await _sut.GetCustomerRedirectByIDAsync(customerRedirectID).ConfigureAwait(false);

            _unitOfWorkMock.Verify(x => x.CustomerRedirectRepository.GetModelSingleOrDefaultAsync(
                It.IsAny<Expression<Func<Entity.CustomerRedirect, CustomerRedirect>>>(),
                It.IsAny<Expression<Func<Entity.CustomerRedirect, bool>>>(),
                null), Times.Once);
        }

        [Fact]
        private async Task GetCustomerRedirectsForCustomerAsync_VerifyCall()
        {
            _unitOfWorkMock.Setup(x => x.CustomerRedirectRepository.GetModelAsync(
                It.IsAny<Expression<Func<Entity.CustomerRedirect, CustomerRedirect>>>(),
                It.IsAny<Expression<Func<Entity.CustomerRedirect, bool>>>(),
                It.IsAny<Func<IQueryable<Entity.CustomerRedirect>, IOrderedQueryable<Entity.CustomerRedirect>>>(),
                0,
                It.IsAny<int>()
                ));

            await _sut.GetCustomerRedirectsAsync().ConfigureAwait(false);

            _unitOfWorkMock.Verify(x => x.CustomerRedirectRepository.GetModelAsync(
                It.IsAny<Expression<Func<Entity.CustomerRedirect, CustomerRedirect>>>(),
                It.IsAny<Expression<Func<Entity.CustomerRedirect, bool>>>(),
                It.IsAny<Func<IQueryable<Entity.CustomerRedirect>, IOrderedQueryable<Entity.CustomerRedirect>>>(),
                0,
                It.IsAny<int>()
                ), Times.Once);
        }

        [Fact]
        private async Task CreateCustomerRedirectAsync_CustomerRedirectIsActive_VerifyDeactivateRedirectCall()
        {
            CustomerRedirect model = new();
            model.IsActive = true;
            _userContextServiceMock.Setup(x => x.CustomerId).Returns(It.IsAny<string>);
            _unitOfWorkMock.Setup(x => x.CustomerRedirectRepository.DeactivateCustomerUrlsAsync(It.IsAny<string>(), 0));

            await _sut.CreateCustomerRedirectAsync(model).ConfigureAwait(false);

            _unitOfWorkMock.Verify(x => x.CustomerRedirectRepository.DeactivateCustomerUrlsAsync(It.IsAny<string>(), It.IsAny<int>()), Times.Once);
            _unitOfWorkMock.Verify(x => x.CustomerRedirectRepository.Insert(It.IsAny<Entity.CustomerRedirect>()), Times.Once);
            _unitOfWorkMock.Verify(x => x.SaveAsync(), Times.Once);
        }

        [Fact]
        private async Task CreateCustomerRedirectAsync_CustomerRedirectIsNotActive_VerifyDeactivateRedirectCallNotCalled()
        {
            CustomerRedirect model = new();
            model.IsActive = false;
            _userContextServiceMock.Setup(x => x.CustomerId).Returns(It.IsAny<string>);
            _unitOfWorkMock.Setup(x => x.CustomerRedirectRepository.DeactivateCustomerUrlsAsync(It.IsAny<string>(), 0));

            await _sut.CreateCustomerRedirectAsync(model).ConfigureAwait(false);

            _unitOfWorkMock.Verify(x => x.CustomerRedirectRepository.DeactivateCustomerUrlsAsync(It.IsAny<string>(), It.IsAny<int>()), Times.Never);
            _unitOfWorkMock.Verify(x => x.CustomerRedirectRepository.Insert(It.IsAny<Entity.CustomerRedirect>()), Times.Once);
            _unitOfWorkMock.Verify(x => x.SaveAsync(), Times.Once);
        }

        [Fact]
        private void UpdateCustomerRedirectAsync_ModelIdIsInvalid_ThrowNotFound()
        {
            CustomerRedirect model = new();
            model.Id = 0;
            _unitOfWorkMock.Setup(x => x.CustomerRedirectRepository.GetModelSingleOrDefaultAsync(
                It.IsAny<Expression<Func<Entity.CustomerRedirect, CustomerRedirect>>>(),
                x => x.CustomerId == It.IsAny<string>() && x.Id == 0,
                null))
                .ReturnsAsync((CustomerRedirect)null);

            Assert.ThrowsAsync<Exception>(async () => await _sut.UpdateCustomerRedirectAsync(model).ConfigureAwait(false));
        }

        [Fact]
        private async Task UpdateCustomerRedirectAsync_ModelIdIsValidAndIsActive_VerifyCall()
        {
            CustomerRedirect model = new();
            model.Id = 1;
            model.IsActive = true;
            _unitOfWorkMock.Setup(x => x.CustomerRedirectRepository.GetModelSingleOrDefaultAsync(
                It.IsAny<Expression<Func<Entity.CustomerRedirect, CustomerRedirect>>>(),
                It.IsAny<Expression<Func<Entity.CustomerRedirect, bool>>>(),
                null))
                .ReturnsAsync(new CustomerRedirect());

            await _sut.UpdateCustomerRedirectAsync(model).ConfigureAwait(false);

            _unitOfWorkMock.Verify(x => x.CustomerRedirectRepository.DeactivateCustomerUrlsAsync(It.IsAny<string>(), It.IsAny<int>()), Times.Once);
            _unitOfWorkMock.Verify(x => x.CustomerRedirectRepository.Update(It.IsAny<Entity.CustomerRedirect>()), Times.Once);
            _unitOfWorkMock.Verify(x => x.SaveAsync(), Times.Exactly(2));
        }

        [Fact]
        private async Task UpdateCustomerRedirectAsync_ModelIdIsValidAndIsNotActive_VerifyCall()
        {
            CustomerRedirect model = new();
            model.Id = 1;
            model.IsActive = false;
            _unitOfWorkMock.Setup(x => x.CustomerRedirectRepository.GetModelSingleOrDefaultAsync(
                It.IsAny<Expression<Func<Entity.CustomerRedirect, CustomerRedirect>>>(),
                It.IsAny<Expression<Func<Entity.CustomerRedirect, bool>>>(),
                null))
                .ReturnsAsync(new CustomerRedirect());

            await _sut.UpdateCustomerRedirectAsync(model).ConfigureAwait(false);

            _unitOfWorkMock.Verify(x => x.CustomerRedirectRepository.DeactivateCustomerUrlsAsync(It.IsAny<string>(), It.IsAny<int>()), Times.Never);
            _unitOfWorkMock.Verify(x => x.CustomerRedirectRepository.Update(It.IsAny<Entity.CustomerRedirect>()), Times.Once);
            _unitOfWorkMock.Verify(x => x.SaveAsync(), Times.Exactly(2));
        }

        [Fact]
        private void DeleteCustomerRedirectAsync_InvalidId_ThrowsException()
        {
            var invalidCustomerRedirectId = 0;
            _unitOfWorkMock.Setup(x => x.CustomerRedirectRepository.GetModelSingleOrDefaultAsync(
                It.IsAny<Expression<Func<Entity.CustomerRedirect, CustomerRedirect>>>(),
                x => x.CustomerId == It.IsAny<string>() && x.Id == 0,
                null))
                .ReturnsAsync((CustomerRedirect)null);

            Assert.ThrowsAsync<Exception>(async () => await _sut.DeleteCustomerRedirectAsync(invalidCustomerRedirectId).ConfigureAwait(false));
        }

        [Fact]
        private async Task DeleteCustomerRedirectAsync_ValidId_VerifyCalls()
        {
            var validCustomerRedirectId = 1;
            _unitOfWorkMock.Setup(x => x.CustomerRedirectRepository.GetModelSingleOrDefaultAsync(
                It.IsAny<Expression<Func<Entity.CustomerRedirect, CustomerRedirect>>>(),
                It.IsAny<Expression<Func<Entity.CustomerRedirect, bool>>>(),
                null))
                .ReturnsAsync(new CustomerRedirect());

            await _sut.DeleteCustomerRedirectAsync(validCustomerRedirectId).ConfigureAwait(false);

            _unitOfWorkMock.Verify(x => x.CustomerRedirectRepository.Delete(It.IsAny<int>()), Times.Once);
            _unitOfWorkMock.Verify(x => x.SaveAsync(), Times.Once);
        }
    }
}