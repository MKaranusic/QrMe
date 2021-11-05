using Microsoft.AspNetCore.Http;

namespace Virgin.Core.Helpers
{
    internal static class QrAddressGenerator
    {
        private const string QrEndpoint = "api/QR";

        public static string Generate(IHttpContextAccessor httpContextAccessor, string guid) => $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}/{QrEndpoint}/{guid}";
    }
}