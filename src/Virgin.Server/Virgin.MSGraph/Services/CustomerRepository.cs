using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Graph;
using Microsoft.Graph.Auth;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Virgin.MSGraph.Configuration;
using Virgin.MSGraph.Constants;
using Virgin.MSGraph.Helpers;
using Virgin.MSGraph.Models;
using Virgin.MSGraph.Services.Interfaces;

namespace Virgin.MSGraph.Services
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly GraphServiceClient _graphClient;
        private readonly MicrosoftGraphConfiguration _graphConfiguration;
        private readonly ILogger<ICustomerRepository> _logger;

        public CustomerRepository(ILogger<ICustomerRepository> logger, IOptions<MicrosoftGraphConfiguration> options)
        {
            IConfidentialClientApplication confidentialClientApplication = ConfidentialClientApplicationBuilder
                .Create(options.Value.AppId)
                .WithTenantId(options.Value.TenantId)
                .WithClientSecret(options.Value.ClientSecret)
                .Build();

            var authProvider = new ClientCredentialProvider(confidentialClientApplication);

            _graphClient = new GraphServiceClient(authProvider);
            _graphConfiguration = options.Value;
            _logger = logger;
        }

        public async Task<QRDetails> CreateCustomerAsync(Customer customer)
        {
            var helper = new B2cCustomAttributeHelper(_graphConfiguration.B2cExtensionAppClientId);
            var userRoleAttributeName = helper.GetCompleteAttributeName("Role");

            IDictionary<string, object> extensionInstance = new Dictionary<string, object>
            {
                { userRoleAttributeName, UserRoles.Customer }
            };

            var qrEntity = new QRDetails
            {
                UserName = UsernameHelper.CreateUsername(customer.GivenName, customer.Surname),
                Password = PasswordHelper.GenerateNewPassword(4, 8, 4),
                Customer = customer
            };

            try
            {
                var result = await _graphClient.Users
                    .Request()
                    .AddAsync(new User
                    {
                        GivenName = customer.GivenName,
                        Surname = customer.Surname,
                        DisplayName = customer.DisplayName,
                        City = customer.City,
                        Mail = customer.Email,
                        PostalCode = customer.PostalCode,
                        StreetAddress = customer.StreetAddress,
                        Identities = new List<ObjectIdentity>
                        {
                                        new ObjectIdentity()
                                        {
                                            SignInType = _graphConfiguration.SignInType,
                                            Issuer = _graphConfiguration.TenantId,
                                            IssuerAssignedId = qrEntity.UserName
                                        }
                        },
                        PasswordProfile = new PasswordProfile()
                        {
                            ForceChangePasswordNextSignIn = false,
                            Password = qrEntity.Password
                        },
                        PasswordPolicies = "DisablePasswordExpiration",
                        AdditionalData = extensionInstance
                    })
                    .ConfigureAwait(false);

                qrEntity.Customer.Id = result.Id;
                return qrEntity;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Method[CreateCustomerAsync], args:{customer}", JsonSerializer.Serialize(customer, new JsonSerializerOptions()));
                throw;
            }
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            var result = await _graphClient.Users
                    .Request()
                    .Filter($"mail eq '{email}'")
                    .GetAsync()
                    .ConfigureAwait(false);

            return result.Count > 0;
        }

        public async Task<bool> UserExistsAsync(string userId)
        {
            try
            {
                var result = await _graphClient.Users
                    .Request()
                    .Filter($"id eq '{userId}'")
                    .GetAsync()
                    .ConfigureAwait(false);

                return result.Count > 0;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Method[UserExistsAsync], args:{userId}", userId);
                return false;
            }
        }
    }
}