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
        public ServiceResponse GetProducts();

        public ServiceResponse AddProduct(ProductDTO product);

        public ServiceResponse UpdateProduct(ProductDTO product);

        public ServiceResponse GetProductById(int id);

        public ServiceResponse GetSellingPriceByItem(int id);
        


    }
}
