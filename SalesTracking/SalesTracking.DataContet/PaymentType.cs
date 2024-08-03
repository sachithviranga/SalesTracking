using System;
using System.Collections.Generic;

namespace SalesTracking.DataContext
{
    public partial class PaymentType
    {
        public PaymentType()
        {
            Payments = new HashSet<Payments>();
            StockPurchasePayment = new HashSet<StockPurchasePayment>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string UpdateBy { get; set; }

        public virtual ICollection<Payments> Payments { get; set; }
        public virtual ICollection<StockPurchasePayment> StockPurchasePayment { get; set; }
    }
}
