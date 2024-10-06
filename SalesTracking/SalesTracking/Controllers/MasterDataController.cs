using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesTracking.Contracts.Managers;
using SalesTracking.Entities.Common;
using System.Threading.Tasks;

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
        public async Task<IActionResult> GetPaymentTypes()
        {
            return Ok(await _masterDataManager.GetPaymentTypes());
        }

        [HttpGet("GetCustomerTypes")]
        public async Task<IActionResult> GetCustomerTypes()
        {
            return Ok(await _masterDataManager.GetCustomerTypes());
        }

        [HttpGet("GetModules")]

        public async Task<IActionResult> GetModules()
        {
            return Ok(await _masterDataManager.GetModules());
        }

    }
}
