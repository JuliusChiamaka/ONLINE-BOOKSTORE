using AutoMapper;
using OnlineBookstore.Application.Interfaces;
using OnlineBookstore.Application.Interfaces.Repository;
using OnlineBookstore.Application.Interfaces.Service;
using OnlineBookstore.Domain.Dtos.Request;
using OnlineBookstore.Domain.Dtos.Response;
using OnlineBookstore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookstore.Application.Services 
{
    public class PurchaseHistoryService : IPurchaseHistoryService
    {
        private readonly IPurchaseHistoryRepository _purchaseHistoryRepository;
        private readonly IMapper _mapper; 

        public PurchaseHistoryService(IPurchaseHistoryRepository purchaseHistoryRepository, IMapper mapper)
        {
            _purchaseHistoryRepository = purchaseHistoryRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PurchaseHistoryResponse>> GetPurchaseHistoryByUserIdAsync(int userId)
        {
            var purchaseHistories = await _purchaseHistoryRepository.GetPurchaseHistoryByUserIdAsync(userId);
            return _mapper.Map<IEnumerable<PurchaseHistoryResponse>>(purchaseHistories);
        }

        public async Task<PurchaseHistoryResponse> GetPurchaseHistoryWithItemsAsync(int id)
        {
            var purchaseHistoryDto = await _purchaseHistoryRepository.GetPurchaseHistoryWithItemsAsync(id);
            return _mapper.Map<PurchaseHistoryResponse>(purchaseHistoryDto);
        }

        public async Task AddPurchaseHistoryAsync(AddPurchaseHistoryRequest request)
        {
            var purchaseHistory = _mapper.Map<PurchaseHistory>(request);
            await _purchaseHistoryRepository.AddAsync(purchaseHistory);
        }
    }

}
