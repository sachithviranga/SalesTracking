using SalesTracking.Common.Common;
using SalesTracking.Contracts.Common;
using SalesTracking.Contracts.Managers;
using SalesTracking.Contracts.Repositories;
using SalesTracking.Entities.Common;
using SalesTracking.Entities.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTracking.Business.Managers
{
    public  class PaymentsManager : IPaymentsManager
    {
        private readonly IPaymentsRepository _paymentsRepository;

        private readonly IMapper<Object, ServiceResponse> _serviceResponseMapper;

        public PaymentsManager(IPaymentsRepository paymentsRepository, IMapper<object, ServiceResponse> serviceResponseMapper)
        {
            _paymentsRepository = paymentsRepository;
            _serviceResponseMapper = serviceResponseMapper;
        }

        public async Task<ServiceResponse> GetPayments()
        {
            var user = UserContext.Current;
            var returnObj = _paymentsRepository.GetPayments();
            return _serviceResponseMapper.Map(returnObj);
        }

        public async Task<ServiceResponse> AddPayments(PaymentDTO payments)
        {
            payments.CreateDate = DateTime.UtcNow; 
            payments.CreateBy = UserContext.Current;
            return _serviceResponseMapper.Map(_paymentsRepository.AddPayments(payments));

        }

        public async Task<ServiceResponse> Updatepayments(PaymentDTO payments)
        {
            payments.UpdateDate = DateTime.UtcNow;
            payments.UpdateBy = "system";
            return _serviceResponseMapper.Map(_paymentsRepository.UpdatePayments(payments));
        }
    }
}
