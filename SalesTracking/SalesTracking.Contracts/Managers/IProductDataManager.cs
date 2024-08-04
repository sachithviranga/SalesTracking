using SalesTracking.Entities.Common;
using SalesTracking.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTracking.Contracts.Managers
{
    public  interface IProductDataManager
    {
        public Task<ServiceResponse> GetProducts();

        public Task<ServiceResponse> AddProduct(ProductDTO product);

        public Task<ServiceResponse> UpdateProduct(ProductDTO product);

        public Task<ServiceResponse> GetProductById(int id);

        public Task<ServiceResponse> GetSellingPriceByItem(int id);
        


    }
}
