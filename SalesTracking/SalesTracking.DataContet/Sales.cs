using System;
using System.Collections.Generic;

namespace SalesTracking.DataContext
{
    public partial class Sales
    {
        public Sales()
        {
            Payments = new HashSet<Payments>();
            SalesDetails = new HashSet<SalesDetails>();
        }

        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime TransactionDate { get; set; }
        public string InvoiceNo { get; set; }
        public bool IsApproved { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public string ApprovedBy { get; set; }
        public decimal TotalAmout { get; set; }
        public decimal TotalPayment { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual ICollection<Payments> Payments { get; set; }
        public virtual ICollection<SalesDetails> SalesDetails { get; set; }
    }
}
