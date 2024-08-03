using System;
using System.Collections.Generic;

namespace SalesTracking.DataContext
{
    public partial class Payments
    {
        public int Id { get; set; }
        public string InvoiceNo { get; set; }
        public int PaymentTypeId { get; set; }
        public decimal? ChequeNo { get; set; }
        public DateTime? ChequeDate { get; set; }
        public decimal Amount { get; set; }
        public int SalesId { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string UpdateBy { get; set; }

        public virtual PaymentType PaymentType { get; set; }
        public virtual Sales Sales { get; set; }
    }
}
