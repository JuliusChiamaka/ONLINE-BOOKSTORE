using OnlineBookstore.Domain.Entities;

namespace OnlineBookstore.Service.Contract.Base
{
    public interface IPurchaseHistoryRepository
    {
        Task<IEnumerable<PurchaseHistory>> GetPurchaseHistoryByUserIdAsync(int userId);
        Task AddPurchaseHistoryAsync(PurchaseHistory history);
    }
}
