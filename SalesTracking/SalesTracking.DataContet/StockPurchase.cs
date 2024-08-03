using System;
using System.Collections.Generic;

namespace SalesTracking.DataContext
{
    public partial class StockPurchase
    {
        public StockPurchase()
        {
            StockPurchaseDetails = new HashSet<StockPurchaseDetails>();
            StockPurchasePayment = new HashSet<StockPurchasePayment>();
        }

        public int Id { get; set; }
        public DateTime TransactionDate { get; set; }
        public string PurchaseNo { get; set; }
        public bool IsApproved { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public string ApprovedBy { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalPayment { get; set; }

        public virtual ICollection<StockPurchaseDetails> StockPurchaseDetails { get; set; }
        public virtual ICollection<StockPurchasePayment> StockPurchasePayment { get; set; }
    }
}
