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
        public Task<List<PaymentDTO>> GetPayments();

        public Task<int> AddPayments(PaymentDTO payments);

        public Task<PaymentDTO> UpdatePayments(PaymentDTO payments);
    }
}
