using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesTracking.Contracts.Managers;
using SalesTracking.Entities.Common;
using SalesTracking.Entities.Customer;
using SalesTracking.Entities.Product;
using System.Threading.Tasks;

namespace SalesTracking.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductDataController : ControllerBase
    {
        private readonly IProductDataManager _productDataRepository;

        public ProductDataController(IProductDataManager productDataRepository)
        {
            _productDataRepository = productDataRepository;
        }
        [HttpGet("GetProducts")]
        public async Task<IActionResult> GetProducts()
        {
            return Ok(await _productDataRepository.GetProducts());
        }

        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct([FromBody] ProductDTO product)
        {
            return Ok(await _productDataRepository.AddProduct(product));
        }

        [HttpPost("UpdateProduct")]

        public async Task<IActionResult> UpdateProduct([FromBody] ProductDTO product)
        {
            return Ok(await _productDataRepository.UpdateProduct(product));

        }

        [HttpGet("GetProductById")]
        public async Task<IActionResult> GetProductById(int id)
        {
            return Ok(await _productDataRepository.GetProductById(id));
        }

        [HttpGet("GetSellingPriceByItem")]
        public async Task<IActionResult> GetSellingPriceByItem(int id)
        {
            return Ok(await _productDataRepository.GetSellingPriceByItem(id));
        }
    }
}
