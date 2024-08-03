using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTracking.Entities.Stock
{
    public class StockBalanceDTO : BaseDTO
    {
        public int ReferenceType { get; set; }
        public int ReferenceId { get; set; }
        public int ReferenceLineId { get; set; }
        public string BatchId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Qty { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal SellPrice { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
