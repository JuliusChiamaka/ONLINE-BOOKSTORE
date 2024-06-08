using Moq;
using OnlineBookstore.Application.Services;
using OnlineBookstore.Domain.Entities;
using OnlineBookstore.Service.Contract.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookstore.Test
{
    public class PurchaseHistoryServiceTests
    {
        private readonly Mock<IPurchaseHistoryRepository> _mockRepo;
        private readonly PurchaseHistoryService _historyService;

        public PurchaseHistoryServiceTests()
        {
            _mockRepo = new Mock<IPurchaseHistoryRepository>();
            _historyService = new PurchaseHistoryService(_mockRepo.Object);
        }

        [Fact]
        public async Task GetPurchaseHistoryByUserIdAsync_ReturnsHistory()
        {
            _mockRepo.Setup(repo => repo.GetPurchaseHistoryByUserIdAsync(1)).ReturnsAsync(new List<PurchaseHistory> { new PurchaseHistory { UserId = 1 } });

            var result = await _historyService.GetPurchaseHistoryByUserIdAsync(1);

            var resultList = result.ToList(); // Convert to list for indexing

            Assert.Single(resultList);
            Assert.Equal(1, resultList[0].UserId);
        }

        [Fact]
        public async Task GetPurchaseHistoryByUserIdAsync_ThrowsArgumentException_WhenUserIdIsInvalid()
        {
            await Assert.ThrowsAsync<ArgumentException>(() => _historyService.GetPurchaseHistoryByUserIdAsync(0));
        }

        // Other tests...
    }
}
