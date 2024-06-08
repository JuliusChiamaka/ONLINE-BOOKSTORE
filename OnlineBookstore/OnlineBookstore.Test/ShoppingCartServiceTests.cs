using Moq;
using OnlineBookstore.Application.Services;
using OnlineBookstore.Domain.Entities;
using OnlineBookstore.Service.Contract.Base;

namespace OnlineBookstore.Test
{
    public class ShoppingCartServiceTests
    {
        private readonly Mock<IShoppingCartRepository> _mockRepo;
        private readonly ShoppingCartService _cartService;

        public ShoppingCartServiceTests()
        {
            _mockRepo = new Mock<IShoppingCartRepository>();
            _cartService = new ShoppingCartService(_mockRepo.Object);
        }

        [Fact]
        public async Task GetCartByUserIdAsync_ReturnsCart()
        {
            var expectedCart = new List<ShoppingCart> { new ShoppingCart { UserId = 1 } };
            _mockRepo.Setup(repo => repo.GetCartByUserIdAsync(1)).ReturnsAsync(expectedCart);

            var result = await _cartService.GetCartByUserIdAsync(1);
            var resultList = result.ToList(); // Convert to list for indexing

            Assert.Single(resultList);
            Assert.Equal(1, resultList[0].UserId);
        }

        [Fact]
        public async Task AddToCartAsync_ThrowsArgumentNullException_WhenCartIsNull()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _cartService.AddToCartAsync(null));
        }
    }
}