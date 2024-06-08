using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineBookstore.Application.Interfaces;
using OnlineBookstore.Domain.Entities;
using OnlineBookstore.Service.Contract.Base;

namespace OnlineBookstore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseHistoryController : ControllerBase
    {
        private readonly IPurchaseHistoryService _historyService;

        public PurchaseHistoryController(IPurchaseHistoryService historyService)
        {
            _historyService = historyService;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<PurchaseHistory>>> GetPurchaseHistory(int userId)
        {
            try
            {
                return Ok(await _historyService.GetPurchaseHistoryByUserIdAsync(userId));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
