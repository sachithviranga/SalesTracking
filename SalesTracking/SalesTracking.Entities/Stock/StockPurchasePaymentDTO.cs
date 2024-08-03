using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTracking.Entities.Stock
{
    public class StockPurchasePaymentDTO : BaseDTO
    {
        public int StockPurchaseId { get; set; }
        public int PaymentTypeId { get; set; }
        public string PaymentTypeName { get; set; }
        public decimal? ChequeNo { get; set; }
        public DateTime? ChequeDate { get; set; }
        public decimal Amount { get; set; }
        public string Note { get; set; }
        public bool IsApproved { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public string ApprovedBy { get; set; }
    }
}
