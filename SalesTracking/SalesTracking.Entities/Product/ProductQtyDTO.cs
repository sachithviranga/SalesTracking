using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTracking.Entities.Product
{
    public class ProductQtyDTO
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public decimal Qty { get; set; }
    }
}
