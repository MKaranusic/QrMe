using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Threading.Tasks;
using Virgin.Core.Configuration;
using Virgin.Core.Services;
using Virgin.Core.Services.Interfaces;
using Virgin.Infrastructure.DAL.Interfaces;
using Virgin.MSGraph.Services.Interfaces;
using Xunit;

namespace Virgin.Core.Test.Services
{
#pragma warning disable IDE0051

    public class QrRedirectServiceTests
    {
        private readonly Mock<IVirginUnitOfWork> _unitOfWorkMock;
        private readonly Mock<ICustomerRepository> _customerRepositoryMock;
        private readonly Mock<ICustomerRedirectService> _customerRedirectServiceMock;
        private readonly Mock<IOptions<ClientConfiguration>> _clientOptionsMock;
        private readonly QrRedirectService _sut;

        public QrRedirectServiceTests()
        {
            _unitOfWorkMock = new();
            _customerRepositoryMock = new();
            _customerRedirectServiceMock = new();
            _clientOptionsMock = new();

            _sut = new(_unitOfWorkMock.Object, _customerRepositoryMock.Object, _customerRedirectServiceMock.Object, _clientOptionsMock.Object);
        }

        [Fact]
        private void GetRedirectUriAsync_InvalidCustomerId_ThrowsException()
        {
            var invalidCustomerId = "InvalidId";
            _customerRepositoryMock.Setup(x => x.UserExistsAsync(invalidCustomerId)).ReturnsAsync(false);

            Assert.ThrowsAsync<Exception>(async () => await _sut.GetRedirectUriAsync(invalidCustomerId).ConfigureAwait(false));
        }

        [Fact]
        private async Task GetRedirectUriAsync_ValidCustomerId_ReturnsUrl()
        {
            var testUri = "http://test.com";
            var validCustomerId = "ValidId";
            _customerRepositoryMock.Setup(x => x.UserExistsAsync(validCustomerId)).ReturnsAsync(true);
            _unitOfWorkMock.Setup(x => x.CustomerRedirectRepository.GetActiveCustomerUrlAsync(validCustomerId)).ReturnsAsync(testUri);

            var result = await _sut.GetRedirectUriAsync(validCustomerId).ConfigureAwait(false);

            _unitOfWorkMock.Verify(x => x.CustomerRedirectRepository.GetActiveCustomerUrlAsync(validCustomerId), Times.Once);
            _customerRedirectServiceMock.Verify(x => x.IncrementActiveCustomerUrlTimesViewedAsync(validCustomerId), Times.Once);
            Assert.Equal(new Uri(testUri).AbsoluteUri, result.AbsoluteUri);
        }
    }
}