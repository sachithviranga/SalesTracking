using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesTracking.Business.Managers;
using SalesTracking.Contracts.Managers;
using SalesTracking.Entities.Common;
using SalesTracking.Entities.Customer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesTracking.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomerDataController : ControllerBase
    {
        private readonly ICustomerDataManager _customerDataRepository;

        public CustomerDataController(ICustomerDataManager customerDataRepository)
        {
            _customerDataRepository = customerDataRepository;
        }


        [HttpGet("GetCustomers")]
        [ProducesResponseType(typeof(List<CustomerDTO>), 200)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetCustomers()
        {
            return Ok(await _customerDataRepository.GetCustomers());
        }

        [HttpPost("AddCustomer")]
        [ProducesResponseType(typeof(int), 200)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> AddCustomer([FromBody] CustomerDTO customer)
        {
            return Ok(await _customerDataRepository.AddCustomer(customer));
        }

        [HttpPost("UpdateCustomer")]
        [ProducesResponseType(typeof(CustomerDTO), 200)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdateCustomer([FromBody] CustomerDTO customer)
        {
            return Ok(await _customerDataRepository.UpdateCustomer(customer));

        }

        [HttpGet("GetCustomerById")]
        [ProducesResponseType(typeof(CustomerDTO), 200)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            return Ok(await _customerDataRepository.GetCustomerById(id));
        }

    }
}
