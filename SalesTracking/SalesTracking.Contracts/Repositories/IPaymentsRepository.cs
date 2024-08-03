using SalesTracking.Entities.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTracking.Contracts.Repositories
{
    public  interface IPaymentsRepository
    {
        public List<PaymentDTO> GetPayments();

        public int AddPayments(PaymentDTO payments);

        public PaymentDTO UpdatePayments(PaymentDTO payments);
    }
}
