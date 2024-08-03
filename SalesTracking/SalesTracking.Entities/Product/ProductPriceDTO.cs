using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTracking.Entities.Product
{
    public class ProductPriceDTO : BaseDTO
    {
        public int ProductId { get; set; }
        public decimal SellPrice { get; set; }
        public decimal UnitPrice { get; set; }
        public DateTime StartData { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsCurrent { get; set; }
    }
}
