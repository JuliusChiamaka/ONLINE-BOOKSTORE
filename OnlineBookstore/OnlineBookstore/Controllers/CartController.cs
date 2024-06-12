using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineBookstore.Application.Interfaces.Service;
using OnlineBookstore.Application.Services;
using OnlineBookstore.Domain.Dtos.Request;


namespace OnlineBookstore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("getCart/{userId}")]
        public async Task<IActionResult> GetCartItems(int userId)
        {
            var cartItems = await _cartService.GetCartItemsByUserIdAsync(userId);
            return Ok(cartItems);
        }

        [HttpPost("addToCart")]
        public async Task<IActionResult> AddToCart(AddToCartRequest request)
        {
            await _cartService.AddToCartAsync(request);
            return Ok(); 
        }

        [HttpDelete("removeCart/{cartItemId}")]
        public async Task<IActionResult> RemoveFromCart(int cartItemId)
        {
            await _cartService.RemoveFromCartAsync(cartItemId);
            return NoContent();
        }

        [HttpPost("checkout/{userId}")]
        public async Task<IActionResult> Checkout(int userId, CheckoutRequest request)
        {
            var result = await _cartService.Checkout(userId, request);
            return Ok(result);
        }
    }

}
