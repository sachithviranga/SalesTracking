using SalesTracking.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTracking.Entities.Sales
{
    public class SalesDTO : BaseDTO 
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public DateTimeOffset TransactionDate { get; set; }
        public string InvoiceNo { get; set; }
        public bool IsApproved { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public string ApprovedBy { get; set; }
        public decimal TotalAmout { get; set; }
        public decimal TotalPayment { get; set; }
        public List<SalesDetailsDTO> SalesDetails { get; set; }
        public List<PaymentsDTO> Payments { get; set; }
       
    }
}
