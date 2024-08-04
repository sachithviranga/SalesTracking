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
        public async Task<ServiceResponse> GetProducts()
        {
            return await _productDataRepository.GetProducts();
        }

        [HttpPost("AddProduct")]
        public async Task<ServiceResponse> AddProduct([FromBody] ProductDTO product)
        {
            return await _productDataRepository.AddProduct(product);
        }

        [HttpPost("UpdateProduct")]

        public async Task<ServiceResponse> UpdateProduct([FromBody]ProductDTO product ) 
        {
            return _productDataRepository.UpdateProduct(product);

        }

        [HttpGet("GetProductById")]
        public async Task<ServiceResponse> GetProductById(int id)
        {
            return _productDataRepository.GetProductById(id);
        }

        [HttpGet("GetSellingPriceByItem")]
        public async Task<ServiceResponse> GetSellingPriceByItem(int id) 
        {
            return _productDataRepository.GetSellingPriceByItem(id);
        }





    }
}
