using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTracking.Entities.Payment
{
    public class PaymentDTO : BaseDTO
    {
        public string InvoiceNo { get; set; }
        public int PaymentTypeId { get; set; }
        public decimal? ChequeNo { get; set; }
        public int SalesId { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
