using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTracking.Entities.Sales
{
    public class PaymentsDTO :BaseDTO
    {
        public string InvoiceNo { get; set; }
        public int PaymentTypeId { get; set; }
        public string PaymentTypeName { get; set; }
        public decimal? ChequeNo { get; set; }
        public DateTime? ChequeDate { get; set; }
        public decimal Amount { get; set; }
        public int SalesId { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
