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
    public class PurchaseHistoryService : IPurchaseHistoryService
    {
        private readonly IPurchaseHistoryRepository _historyRepository;

        public PurchaseHistoryService(IPurchaseHistoryRepository historyRepository)
        {
            _historyRepository = historyRepository;
        }

        public async Task<IEnumerable<PurchaseHistory>> GetPurchaseHistoryByUserIdAsync(int userId)
        {
            if (userId <= 0)
            {
                throw new ArgumentException("Invalid user ID");
            }

            return await _historyRepository.GetPurchaseHistoryByUserIdAsync(userId);
        }
    }
}
