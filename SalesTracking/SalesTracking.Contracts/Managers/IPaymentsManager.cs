using SalesTracking.Entities.Common;
using SalesTracking.Entities.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTracking.Contracts.Managers
{
    public interface IPaymentsManager
    {
        public Task<ServiceResponse> GetPayments();

        public Task<ServiceResponse> AddPayments(PaymentDTO payments);

        public Task<ServiceResponse> Updatepayments(PaymentDTO payments);
    }
}
