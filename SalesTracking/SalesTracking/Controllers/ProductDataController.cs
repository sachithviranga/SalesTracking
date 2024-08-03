using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesTracking.Contracts.Managers;
using SalesTracking.Entities.Common;
using SalesTracking.Entities.Customer;
using SalesTracking.Entities.Product;

namespace SalesTracking.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [Authorize]
    public class ProductDataController : ControllerBase
    {
        private readonly IProductDataManager _productDataRepository;

        public ProductDataController(IProductDataManager productDataRepository)
        {
            _productDataRepository = productDataRepository;
        }
        [HttpGet("GetProducts")]
        public ServiceResponse GetProducts()
        {
            return _productDataRepository.GetProducts();
        }

        [HttpPost("AddProduct")]
        public ServiceResponse AddProduct([FromBody] ProductDTO product)
        {
            return _productDataRepository.AddProduct(product);
        }

        [HttpPost("UpdateProduct")]

        public ServiceResponse UpdateProduct([FromBody]ProductDTO product ) 
        {
            return _productDataRepository.UpdateProduct(product);

        }

        [HttpGet("GetProductById")]
        public ServiceResponse GetProductById(int id)
        {
            return _productDataRepository.GetProductById(id);
        }

        [HttpGet("GetSellingPriceByItem")]
        public ServiceResponse GetSellingPriceByItem(int id) 
        {
            return _productDataRepository.GetSellingPriceByItem(id);
        }





    }
}
