using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTracking.Entities.Stock
{
    public class StockPurchaseDTO : BaseDTO
    {
        public string PurchaseNo { get; set; }
        public DateTimeOffset TransactionDate { get; set; }
        public bool IsApproved { get; set; }
        public DateTime ApprovedDate { get; set; }
        public string ApprovedBy { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalPayment { get; set; }

        public List<StockPurchaseDetailsDTO> StockPurchaseDetails { get; set; }
        public List<StockPurchasePaymentDTO> StockPurchasePayment { get; set; }

    }
}
