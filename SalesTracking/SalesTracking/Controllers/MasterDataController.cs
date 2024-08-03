using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesTracking.Contracts.Managers;
using SalesTracking.Entities.Common;

namespace SalesTracking.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MasterDataController : ControllerBase
    {
        private readonly IMasterDataManager _masterDataManager;

        public MasterDataController(IMasterDataManager masterDataManager)
        {
            _masterDataManager = masterDataManager;
        }

        [HttpGet("GetPaymentTypes")]
        public ServiceResponse GetPaymentTypes()
        {
            return _masterDataManager.GetPaymentTypes();
        }

        [HttpGet("GetCustomerTypes")]
        public ServiceResponse GetCustomerTypes()
        {
            return _masterDataManager.GetCustomerTypes();
        }

        [HttpGet("GetModules")]

        public ServiceResponse GetModules()
        {
            return _masterDataManager.GetModules();
        }

    }
}
