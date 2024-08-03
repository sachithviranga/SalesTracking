using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesTracking.Business.Managers;
using SalesTracking.Contracts.Managers;
using SalesTracking.Entities.Common;
using SalesTracking.Entities.Customer;

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
        public ServiceResponse GetCustomers()
        {
            return _customerDataRepository.GetCustomers();
        }

        [HttpPost("AddCustomer")]
        public ServiceResponse AddCustomer([FromBody]CustomerDTO customer )
        {
            return _customerDataRepository.AddCustomer(customer);
        }

        [HttpPost("UpdateCustomer")]

        public ServiceResponse UpdateCustomer([FromBody] CustomerDTO customer)
        {
            return _customerDataRepository.UpdateCustomer(customer);

        }

        [HttpGet("GetCustomerById")]
        public ServiceResponse GetCustomerById(int id)
        {
            return _customerDataRepository.GetCustomerById(id);
        }

    }
}
