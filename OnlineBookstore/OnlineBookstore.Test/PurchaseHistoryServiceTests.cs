using AutoMapper;
using Moq;
using OnlineBookstore.Application.Interfaces.Repository;
using OnlineBookstore.Application.Services;
using OnlineBookstore.Domain.Dtos.Request;
using OnlineBookstore.Domain.Entities;
using OnlineBookstore.Infrastructure.Configs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookstore.Test
{
    public class PurchaseHistoryServiceTests
    {
        private readonly Mock<IPurchaseHistoryRepository> _mockPurchaseHistoryRepository;
        private readonly IMapper _mapper;
        private readonly PurchaseHistoryService _purchaseHistoryService;

        public PurchaseHistoryServiceTests()
        {
            _mockPurchaseHistoryRepository = new Mock<IPurchaseHistoryRepository>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = config.CreateMapper();

            _purchaseHistoryService = new PurchaseHistoryService(_mockPurchaseHistoryRepository.Object, _mapper);
        }

        [Fact]
        public async Task GetPurchaseHistoryByUserIdAsync_ShouldReturnPurchaseHistory()
        {
            // Arrange
            var userId = 1;
            var purchaseHistories = TestData.GetPurchaseHistories();
            _mockPurchaseHistoryRepository.Setup(repo => repo.GetPurchaseHistoryByUserIdAsync(userId)).ReturnsAsync(purchaseHistories);

            // Act
            var result = await _purchaseHistoryService.GetPurchaseHistoryByUserIdAsync(userId);

            // Assert
            Assert.Equal(purchaseHistories.Count, result.Count());
        }

        [Fact]
        public async Task GetPurchaseHistoryWithItemsAsync_ShouldReturnPurchaseHistoryWithItems()
        {
            // Arrange
            var purchaseHistory = TestData.GetPurchaseHistories().First();
            _mockPurchaseHistoryRepository.Setup(repo => repo.GetPurchaseHistoryWithItemsAsync(purchaseHistory.Id)).ReturnsAsync(purchaseHistory);

            // Act
            var result = await _purchaseHistoryService.GetPurchaseHistoryWithItemsAsync(purchaseHistory.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(purchaseHistory.Id, result.Id);
        }

        [Fact]
        public async Task AddPurchaseHistoryAsync_ShouldAddPurchaseHistory()
        {
            // Arrange
            var request = new AddPurchaseHistoryRequest
            {
                UserId = 1,
                TotalAmount = 50.0m,
                Items = new List<AddPurchaseHistoryItemRequest>
            {
                new AddPurchaseHistoryItemRequest { BookId = 1, Quantity = 1, Price = 50.0m }
            }
            };
            var purchaseHistory = _mapper.Map<PurchaseHistory>(request);

            _mockPurchaseHistoryRepository.Setup(repo => repo.AddAsync(It.IsAny<PurchaseHistory>())).Returns(Task.CompletedTask);

            // Act
            await _purchaseHistoryService.AddPurchaseHistoryAsync(request);

            // Assert
            _mockPurchaseHistoryRepository.Verify(repo => repo.AddAsync(It.Is<PurchaseHistory>(ph => ph.UserId == request.UserId && ph.TotalAmount == request.TotalAmount)), Times.Once);
        }
    }

}
