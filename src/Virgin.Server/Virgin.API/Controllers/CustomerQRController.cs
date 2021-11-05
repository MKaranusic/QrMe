using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Virgin.API.DTOs;
using Virgin.Core.Models;
using Virgin.Core.Services.Interfaces;

namespace Virgin.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = VirginAuthorizationConstants.AdminPolicy)]
    public class CustomerQRController : ControllerBase
    {
        private readonly ICustomerQRService _customerQRService;

        public CustomerQRController(ICustomerQRService customerQRService)
        {
            _customerQRService = customerQRService;
        }

        [HttpPost]
        public async Task<ActionResult<QRDetails>> CreateCustomerQRAsync(CustomerDTO customer)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var result = await _customerQRService.CreateCustomerQRAsync(CustomerDTO.ToDomain(customer));

            return Ok(result);
        }
    }
}