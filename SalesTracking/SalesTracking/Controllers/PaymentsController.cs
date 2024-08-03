using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesTracking.Contracts.Managers;
using SalesTracking.Entities.Common;
using SalesTracking.Entities.Payment;

namespace SalesTracking.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentsManager _paymentsrepository;

        public PaymentsController(IPaymentsManager paymentsrepository)
        {
            _paymentsrepository = paymentsrepository;
        }


        [HttpGet("GetPayments")]
        public ServiceResponse GetPayments()
        {
            return _paymentsrepository.GetPayments();
        }

        [HttpPost("AddPayments")]
        public ServiceResponse AddPayments([FromBody] PaymentDTO payments)
        {
            return _paymentsrepository.AddPayments(payments);
        }

        [HttpPatch("UpdatePayments")]

        public ServiceResponse UpdatePayments([FromBody] PaymentDTO payments)
        {
            return _paymentsrepository.Updatepayments(payments);

        }
    }
}
