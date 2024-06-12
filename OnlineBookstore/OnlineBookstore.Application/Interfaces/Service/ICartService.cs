using OnlineBookstore.Domain.Dtos.Request;
using OnlineBookstore.Domain.Dtos.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace OnlineBookstore.Application.Interfaces.Service
{
    public interface ICartService
    {
        Task<IEnumerable<CartItemResponse>> GetCartItemsByUserIdAsync(int userId);
        Task AddToCartAsync(AddToCartRequest request);
        Task RemoveFromCartAsync(int cartItemId);
        Task<string> Checkout(int userId, CheckoutRequest request);
    }
}
