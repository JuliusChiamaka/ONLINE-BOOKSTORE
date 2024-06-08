using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineBookstore.Application.Interfaces;
using OnlineBookstore.Domain.Entities;
using OnlineBookstore.Service.Contract.Base;

namespace OnlineBookstore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckoutController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public CheckoutController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("{userId}")]
        public async Task<ActionResult> Checkout(int userId, [FromQuery] string paymentMethod)
        {
            try
            {
                var result = await _paymentService.Checkout(userId, paymentMethod);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
