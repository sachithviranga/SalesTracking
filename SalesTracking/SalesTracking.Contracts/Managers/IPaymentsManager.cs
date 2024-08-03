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
        public ServiceResponse GetPayments();

        public ServiceResponse AddPayments(PaymentDTO payments);

        public ServiceResponse Updatepayments(PaymentDTO payments);
    }
}
