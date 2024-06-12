using OnlineBookstore.Application.Interfaces.Repository.Base;
using OnlineBookstore.Domain.Dtos.Request;
using OnlineBookstore.Domain.Entities;

namespace OnlineBookstore.Application.Interfaces.Repository
{
    public interface IPurchaseHistoryRepository : IGenericRepository<PurchaseHistory>
    {
        Task<IEnumerable<PurchaseHistory>> GetPurchaseHistoryByUserIdAsync(int userId);
        Task<PurchaseHistoryDto> GetPurchaseHistoryWithItemsAsync(int id);
    }
}
