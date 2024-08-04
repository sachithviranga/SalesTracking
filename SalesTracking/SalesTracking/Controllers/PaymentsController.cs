using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesTracking.Contracts.Managers;
using SalesTracking.Entities.Common;
using SalesTracking.Entities.Payment;
using System.Threading.Tasks;

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
        public async Task<IActionResult> GetPayments()
        {
            return Ok(await _paymentsrepository.GetPayments());
        }

        [HttpPost("AddPayments")]
        public async Task<IActionResult> AddPayments([FromBody] PaymentDTO payments)
        {
            return Ok(await _paymentsrepository.AddPayments(payments));
        }

        [HttpPatch("UpdatePayments")]

        public async Task<IActionResult>  UpdatePayments([FromBody] PaymentDTO payments)
        {
            return Ok(await _paymentsrepository.Updatepayments(payments));

        }
    }
}
