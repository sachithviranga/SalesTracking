using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesTracking.Contracts.Managers;
using SalesTracking.Entities.Common;
using SalesTracking.Entities.Sales;
using System.Threading.Tasks;

namespace SalesTracking.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class SalesController : ControllerBase
    {
        private readonly ISalesManager _salesmanager;

        public SalesController(ISalesManager salesmanager)
        {
            _salesmanager = salesmanager;
        }

        [HttpGet("GetSales")]
        public async Task<IActionResult> GetSales()
        {
            return Ok(await _salesmanager.GetSales());
        }

        [HttpPost("AddSales")]
        public async Task<IActionResult> AddSales([FromBody] SalesDTO sales)
        {
            return Ok(await _salesmanager.AddSales(sales));
        }

        [HttpPost("UpdateSales")]

        public async Task<IActionResult> UpdateSales([FromBody] SalesDTO sales)
        {
            return Ok(await _salesmanager.UpdateSales(sales));

        }

        [HttpGet("GetSalesById")]

        public async Task<IActionResult> GetSalesById(int id) 
        {
            return Ok(await _salesmanager.GetSalesById(id));
        }

        [HttpPost("ApproveSales")]

        public async Task<IActionResult> ApproveSales([FromBody] SalesDTO sales)
        {
            return Ok(await _salesmanager.ApproveSales(sales));
        }
    }
}
