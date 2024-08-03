using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesTracking.Contracts.Managers;
using SalesTracking.Entities.Common;
using SalesTracking.Entities.Stock;

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
        public ServiceResponse GetStock()
        {
            return _stockmanager.GetStock();
        }


        [HttpPost("AddStock")]
        public ServiceResponse AddStock([FromBody] StockPurchaseDTO stock)
        {
            return _stockmanager.AddStock(stock);
        }

        [HttpPost("UpdateStock")]

        public ServiceResponse UpdateStock([FromBody] StockPurchaseDTO stock)
        {
            return _stockmanager.UpdateStock(stock);

        }

        [HttpGet("GetStockPayment")]
        public ServiceResponse GetStockPayment()
        {
            return _stockmanager.GetStockPayment();
        }

        [HttpGet("GetStockById")]
        public ServiceResponse GetStockById(int id)
        {
            return _stockmanager.GetStockById(id);
        }


        [HttpPost("ApproveStock")]

        public ServiceResponse ApproveStock([FromBody] StockPurchaseDTO stock)
        {
            return _stockmanager.ApproveStock(stock);

        }

        [HttpGet("GetStockBySellprice")]
        public ServiceResponse GetStockBySellprice(int id)
        {
            return _stockmanager.GetStockBySellprice(id);
        }

    }
}
