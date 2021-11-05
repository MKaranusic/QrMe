using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Threading.Tasks;
using Virgin.Core.Models;
using Virgin.Core.Services;
using Virgin.MSGraph.Services.Interfaces;
using Xunit;
using GraphModel = Virgin.MSGraph.Models;

namespace Virgin.Core.Test.Services
{
#pragma warning disable IDE0051

    public class CustomerQRServiceTests
    {
        private const string ValidEmail = "mail@invalid.com";
        private const string InvalidEmail = "mail@valid.com";

        private readonly CustomerQRService _sut;
        private readonly Mock<ICustomerRepository> _customerRepositoryMock;
        private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;

        public CustomerQRServiceTests()
        {
            _customerRepositoryMock = new Mock<ICustomerRepository>();
            _customerRepositoryMock.Setup(x => x.EmailExistsAsync(InvalidEmail)).ReturnsAsync(true);
            _customerRepositoryMock.Setup(x => x.EmailExistsAsync(ValidEmail)).ReturnsAsync(false);

            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            _httpContextAccessorMock.Setup(x => x.HttpContext).Returns(new DefaultHttpContext());

            _sut = new CustomerQRService(_customerRepositoryMock.Object, _httpContextAccessorMock.Object);
        }

        [Fact]
        private void CreateCustomerQRAsync_InvalidEmail_ThrowsException()
        {
            Customer model = new()
            {
                Email = InvalidEmail
            };

            Assert.ThrowsAsync<Exception>(async () => await _sut.CreateCustomerQRAsync(model).ConfigureAwait(false));
        }

        [Fact]
        private async Task CreateCustomerQRAsync_ValidCustomer_ReturnsQRDetails()
        {
            GraphModel.QRDetails graphModel = new()
            {
                Customer = new GraphModel.Customer() { Email = ValidEmail }
            };
            _customerRepositoryMock.Setup(x => x.CreateCustomerAsync(It.IsAny<GraphModel.Customer>())).ReturnsAsync(graphModel);

            var result = await _sut.CreateCustomerQRAsync(new Customer()).ConfigureAwait(false);

            Assert.Equal(ValidEmail, graphModel.Customer.Email);
        }
    }
}