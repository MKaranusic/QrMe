using System.Security.Claims;
using Virgin.Core.Services.Interfaces;

namespace Virgin.Core.Services
{
    public class UserContextService : IUserContextService
    {
        private const string UserUidClaimKey = "http://schemas.microsoft.com/identity/claims/objectidentifier";
        private readonly ClaimsPrincipal _principal;

        public UserContextService(ClaimsPrincipal principal)
        {
            _principal = principal;
        }

        public string CustomerId => _principal.FindFirst(UserUidClaimKey)?.Value;
    }
}