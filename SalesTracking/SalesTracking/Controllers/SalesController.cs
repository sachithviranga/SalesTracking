using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesTracking.Contracts.Managers;
using SalesTracking.Entities.Common;
using SalesTracking.Entities.Sales;

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
        public ServiceResponse GetSales()
        {
            return _salesmanager.GetSales();
        }

        [HttpPost("AddSales")]
        public ServiceResponse AddSales([FromBody] SalesDTO sales)
        {
            return _salesmanager.AddSales(sales);
        }

        [HttpPost("UpdateSales")]

        public ServiceResponse UpdateSales([FromBody] SalesDTO sales)
        {
            return _salesmanager.UpdateSales(sales);

        }

        [HttpGet("GetSalesById")]

        public ServiceResponse GetSalesById(int id) 
        {
            return _salesmanager.GetSalesById(id);
        }

        [HttpPost("ApproveSales")]

        public ServiceResponse ApproveSales([FromBody] SalesDTO sales)
        {
            return _salesmanager.ApproveSales(sales);

        }
    }
}
