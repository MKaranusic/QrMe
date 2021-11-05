using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Virgin.API.DTOs;
using Virgin.Core.Models;
using Virgin.Core.Services.Interfaces;

namespace Virgin.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomerRedirectController : ControllerBase
    {
        private readonly ICustomerRedirectService _customerRedirectService;

        public CustomerRedirectController(ICustomerRedirectService customerRedirectService)
        {
            _customerRedirectService = customerRedirectService;
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateCustomerRedirectAsync(CustomerRedirectDTO customerRedirect)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var result = await _customerRedirectService.CreateCustomerRedirectAsync(CustomerRedirectDTO.ToDomain(customerRedirect));

            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<CustomerRedirect>> GetCustomerRedirectByIDAsync(int id)
        {
            var result = await _customerRedirectService.GetCustomerRedirectByIDAsync(id);

            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<List<CustomerRedirect>>> GetCustomerRedirectsAsync()
        {
            var result = await _customerRedirectService.GetCustomerRedirectsAsync();

            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<List<int>>> UpdateCustomerRedirectAsync(CustomerRedirectDTO customerRedirect)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var result = await _customerRedirectService.UpdateCustomerRedirectAsync(CustomerRedirectDTO.ToDomain(customerRedirect));

            return Ok(result);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<CustomerRedirect>> DeleteCustomerRedirectAsync(int id)
        {
            var result = await _customerRedirectService.DeleteCustomerRedirectAsync(id);

            return Ok(result);
        }

        [HttpGet(("times-viewed"))]
        public async Task<ActionResult<int>> GetCustomerTimesViewedSumAsync()
        {
            var result = await _customerRedirectService.GetCustomerTimesViewedSumAsync();

            return Ok(result);
        }
    }
}