using OnlineBookstore.Application.Interfaces;
using OnlineBookstore.Domain.Entities;
using OnlineBookstore.Service.Contract.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookstore.Application.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IShoppingCartRepository _cartRepository;

        public ShoppingCartService(IShoppingCartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<IEnumerable<ShoppingCart>> GetCartByUserIdAsync(int userId)
        {
            return await _cartRepository.GetCartByUserIdAsync(userId);
        }

        public async Task AddToCartAsync(ShoppingCart cart)
        {
            if (cart == null)
            {
                throw new ArgumentNullException(nameof(cart));
            }

            
            if (cart.UserId <= 0)
            {
                throw new ArgumentException("Invalid user ID");
            }

            await _cartRepository.AddToCartAsync(cart);
        }

        public async Task RemoveFromCartAsync(int id)
        {
            var existingCart = await _cartRepository.GetCartByUserIdAsync(id);
            if (existingCart == null)
            {
                throw new KeyNotFoundException("Cart item not found");
            }

            await _cartRepository.RemoveFromCartAsync(id);
        }
    }
}
