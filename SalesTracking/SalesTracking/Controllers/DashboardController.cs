using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesTracking.Business.Managers;
using SalesTracking.Contracts.Managers;
using SalesTracking.Entities.Common;
using System.Threading.Tasks;

namespace SalesTracking.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardManager _dashboardManager;

        public DashboardController(IDashboardManager dashboardManager)
        {
            _dashboardManager = dashboardManager;
        }

        [HttpGet("GetAvailableProdcuts")]
        public async Task<IActionResult> GetAvailableProdcuts()
        {
            return Ok(await _dashboardManager.GetAvailableProdcuts());
        }
    }
}
