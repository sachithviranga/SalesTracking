using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesTracking.Contracts.Managers;
using SalesTracking.Entities.Common;
using SalesTracking.Entities.Stock;
using System.Threading.Tasks;

namespace SalesTracking.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StockController : ControllerBase
    {
        private readonly IStockManager _stockmanager;

        public StockController(IStockManager stockmanager)
        {
            _stockmanager = stockmanager;
        }

        [HttpGet("GetStock")]
        public async Task<IActionResult> GetStock()
        {
            return Ok(await _stockmanager.GetStock());
        }


        [HttpPost("AddStock")]
        public async Task<IActionResult> AddStock([FromBody] StockPurchaseDTO stock)
        {
            return Ok(await _stockmanager.AddStock(stock));
        }

        [HttpPost("UpdateStock")]

        public async Task<IActionResult> UpdateStock([FromBody] StockPurchaseDTO stock)
        {
            return Ok(await _stockmanager.UpdateStock(stock));

        }

        [HttpGet("GetStockPayment")]
        public async Task<IActionResult> GetStockPayment()
        {
            return Ok(await _stockmanager.GetStockPayment());
        }

        [HttpGet("GetStockById")]
        public async Task<IActionResult> GetStockById(int id)
        {
            return Ok(await _stockmanager.GetStockById(id));
        }


        [HttpPost("ApproveStock")]

        public async Task<IActionResult> ApproveStock([FromBody] StockPurchaseDTO stock)
        {
            return Ok(await _stockmanager.ApproveStock(stock));

        }

        [HttpGet("GetStockBySellprice")]
        public async Task<IActionResult> GetStockBySellprice(int id)
        {
            return Ok(await _stockmanager.GetStockBySellprice(id));
        }

    }
}
