using Microsoft.AspNetCore.Mvc;
using PaymentProcessor.Domain.ViewModels;
using PaymentProcessor.Service.BL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PaymentProcessor.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentService;
        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        /// <summary>
        /// Process Payment
        /// </summary>
        /// <response code="200">Payment successful</response>
        /// <response code="400">Unable to process payment due to validation error(s)</response>
        /// <response code="500">Other error(s)</response>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ActionResponse<PaymentToReturn>))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ProcessPayment([FromBody] PaymentDTO request)
        {
            var response = await _paymentService.Pay(request);
            return Ok(response);
        }
    }
}
