using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTracking.Entities.Product
{
    public class ProductDTO : BaseDTO
    {
        public string Name { get; set; }
        public decimal SellPrice { get; set; }
        public decimal UnitPrice { get; set; }
        public List<ProductPriceDTO> ProductPrice { get; set; }
    }
}
