using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineBookstore.Application.Interfaces;
using OnlineBookstore.Domain.Entities;
using OnlineBookstore.Service.Contract.Base;

namespace OnlineBookstore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartService _cartService;

        public ShoppingCartController(IShoppingCartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("getAllCart/{userId}")]
        public async Task<ActionResult<IEnumerable<ShoppingCart>>> GetCart(int userId)
        {
            return Ok(await _cartService.GetCartByUserIdAsync(userId));
        }

        [HttpPost("addCart")]
        public async Task<ActionResult> AddToCart([FromBody] ShoppingCart cart)
        {
            try
            {
                await _cartService.AddToCartAsync(cart);
                return CreatedAtAction(nameof(GetCart), new { userId = cart.UserId }, cart);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("deleteCart/{id}")]
        public async Task<ActionResult> RemoveFromCart(int id)
        {
            try
            {
                await _cartService.RemoveFromCartAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
