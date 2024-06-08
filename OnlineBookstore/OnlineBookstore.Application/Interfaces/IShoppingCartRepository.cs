using OnlineBookstore.Domain.Entities;

namespace OnlineBookstore.Service.Contract.Base
{
    public interface IShoppingCartRepository
    {
        Task<IEnumerable<ShoppingCart>> GetCartByUserIdAsync(int userId);
        Task AddToCartAsync(ShoppingCart cart);
        Task RemoveFromCartAsync(int id );
    }
}
