using OnlineBookstore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookstore.Application.Interfaces
{
    public interface IShoppingCartService
    {
        Task<IEnumerable<ShoppingCart>> GetCartByUserIdAsync(int userId);
        Task AddToCartAsync(ShoppingCart cart);
        Task RemoveFromCartAsync(int id);
    }
}
