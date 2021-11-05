using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Virgin.Core.Services.Interfaces;

namespace Virgin.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QRController : ControllerBase
    {
        private readonly IQrRedirectService _qrRedirectService;

        public QRController(IQrRedirectService qrRedirectService)
        {
            _qrRedirectService = qrRedirectService;
        }

        [HttpGet]
        [Route("{customerId}")]
        public async Task<RedirectResult> QrAsync(string customerId)
        {
            var result = await _qrRedirectService.GetRedirectUriAsync(customerId);

            return Redirect(result.AbsoluteUri);
        }
    }
}