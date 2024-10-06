using SalesTracking.Entities.Customer;
using SalesTracking.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTracking.Contracts.Repositories
{
    public interface IProductDataRepository
    {
        public Task<List<ProductDTO>> GetProducts();
        public Task<int> AddProduct(ProductDTO product);
        public Task<ProductDTO> UpdateProduct(ProductDTO product);
        public Task<ProductDTO> GetProductById(int id);
        public Task<ProductPriceDTO> GetSellingPriceByItem(int id);
        
    }
}
