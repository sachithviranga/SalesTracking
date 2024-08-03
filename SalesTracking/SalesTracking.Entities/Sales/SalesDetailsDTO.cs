using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTracking.Entities.Sales
{
    public class SalesDetailsDTO : BaseDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal SellPrice { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Qty { get; set; }
        public int SalesId { get; set; }
        public int PriceId { get; set; }    
        public decimal Amount { get; set; }
    }
}
