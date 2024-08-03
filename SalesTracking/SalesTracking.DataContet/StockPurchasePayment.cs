using System;
using System.Collections.Generic;

namespace SalesTracking.DataContext
{
    public partial class StockPurchasePayment
    {
        public int Id { get; set; }
        public int StockPurchaseId { get; set; }
        public int PaymentTypeId { get; set; }
        public decimal? ChequeNo { get; set; }
        public DateTime? ChequeDate { get; set; }
        public decimal Amount { get; set; }
        public string Note { get; set; }
        public bool IsApproved { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public string ApprovedBy { get; set; }

        public virtual PaymentType PaymentType { get; set; }
        public virtual StockPurchase StockPurchase { get; set; }
    }
}
