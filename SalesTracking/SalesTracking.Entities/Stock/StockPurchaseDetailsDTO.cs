using SalesTracking.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTracking.Entities.Stock
{
    public class StockPurchaseDetailsDTO : BaseDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Qty { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal SellPrice { get; set; }
        public int StockPurchaseId { get; set; }
        public decimal Amount { get; set; }
        //public List<ProductDTO> Product { get; set; } = new List<ProductDTO>();
        //public List<StockPurchaseDTO> StockPurchase { get; set; } = new List<StockPurchaseDTO>();

    }

}
