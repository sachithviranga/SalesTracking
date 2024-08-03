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
        public List<ProductDTO> GetProducts();
        public int AddProduct(ProductDTO product);
        public ProductDTO UpdateProduct(ProductDTO product);
        public ProductDTO GetProductById(int id);
        public ProductPriceDTO GetSellingPriceByItem(int id);
        
    }
}
