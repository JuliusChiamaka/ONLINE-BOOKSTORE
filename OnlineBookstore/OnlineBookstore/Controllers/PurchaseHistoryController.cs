using Microsoft.AspNetCore.Mvc;
using OnlineBookstore.Application.Interfaces.Service;
using OnlineBookstore.Domain.Dtos.Request;

namespace OnlineBookstore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PurchaseHistoryController : ControllerBase
    {
        private readonly IPurchaseHistoryService _purchaseHistoryService;

        public PurchaseHistoryController(IPurchaseHistoryService purchaseHistoryService)
        {
            _purchaseHistoryService = purchaseHistoryService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetPurchaseHistoryByUserId(int userId)
        {
            var purchaseHistory = await _purchaseHistoryService.GetPurchaseHistoryByUserIdAsync(userId);
            return Ok(purchaseHistory);
        }

        [HttpGet("details/{id}")]
        public async Task<IActionResult> GetPurchaseHistoryWithItems(int id)
        {
            var purchaseHistory = await _purchaseHistoryService.GetPurchaseHistoryWithItemsAsync(id);
            if (purchaseHistory == null)
            {
                return NotFound();
            }
            return Ok(purchaseHistory);
        }

        [HttpPost("addPurchaseHistory")]
        public async Task<IActionResult> AddPurchaseHistory(AddPurchaseHistoryRequest request)
        {
            await _purchaseHistoryService.AddPurchaseHistoryAsync(request);
            return Ok(); // Return Ok without referencing request.Id
        }
    }

}
