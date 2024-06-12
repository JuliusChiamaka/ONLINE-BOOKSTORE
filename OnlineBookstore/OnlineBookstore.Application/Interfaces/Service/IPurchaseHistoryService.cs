using OnlineBookstore.Domain.Dtos.Request;
using OnlineBookstore.Domain.Dtos.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookstore.Application.Interfaces.Service
{
    public interface IPurchaseHistoryService
    {
        Task<IEnumerable<PurchaseHistoryResponse>> GetPurchaseHistoryByUserIdAsync(int userId);
        Task<PurchaseHistoryResponse> GetPurchaseHistoryWithItemsAsync(int id);
        Task AddPurchaseHistoryAsync(AddPurchaseHistoryRequest request);
    }
}
