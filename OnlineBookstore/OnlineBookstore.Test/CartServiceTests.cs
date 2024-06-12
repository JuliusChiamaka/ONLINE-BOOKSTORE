using AutoMapper;
using Moq;
using OnlineBookstore.Application.Interfaces.Repository;
using OnlineBookstore.Application.Services;
using OnlineBookstore.Domain.Dtos.Request;
using OnlineBookstore.Domain.Entities;
using OnlineBookstore.Infrastructure.Configs;

namespace OnlineBookstore.Test
{
    public class CartServiceTests
    {
        private readonly Mock<ICartRepository> _mockCartRepository;
        private readonly Mock<IBookRepository> _mockBookRepository;
        private readonly IMapper _mapper;
        private readonly CartService _cartService;

        public CartServiceTests()
        {
            _mockCartRepository = new Mock<ICartRepository>();
            _mockBookRepository = new Mock<IBookRepository>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = config.CreateMapper();

            _cartService = new CartService(_mockCartRepository.Object, _mockBookRepository.Object, _mapper);
        }

        [Fact]
        public async Task GetCartItemsByUserIdAsync_ShouldReturnCartItems()
        {
            // Arrange
            var userId = 1;
            var cartItems = TestData.GetCartItems();
            _mockCartRepository.Setup(repo => repo.GetCartItemsByUserIdAsync(userId)).ReturnsAsync(cartItems);

            // Act
            var result = await _cartService.GetCartItemsByUserIdAsync(userId);

            // Assert
            Assert.Equal(cartItems.Count, result.Count());
        }

        [Fact]
        public async Task AddToCartAsync_ShouldAddToCart()
        {
            // Arrange
            var book = TestData.GetBooks().First();
            var request = new AddToCartRequest { UserId = 1, BookId = book.Id, Quantity = 1 };
            var cartItem = _mapper.Map<CartItem>(request);

            _mockBookRepository.Setup(repo => repo.GetByIdAsync(book.Id)).ReturnsAsync(book);
            _mockCartRepository.Setup(repo => repo.AddAsync(It.IsAny<CartItem>())).Returns(Task.CompletedTask);

            // Act
            await _cartService.AddToCartAsync(request);

            // Assert
            _mockCartRepository.Verify(repo => repo.AddAsync(It.Is<CartItem>(ci => ci.BookId == book.Id && ci.Quantity == 1)), Times.Once);
        }

        [Fact]
        public async Task RemoveFromCartAsync_ShouldRemoveFromCart()
        {
            // Arrange
            var cartItemId = 1;

            _mockCartRepository.Setup(repo => repo.DeleteAsync(cartItemId)).Returns(Task.CompletedTask);

            // Act
            await _cartService.RemoveFromCartAsync(cartItemId);

            // Assert
            _mockCartRepository.Verify(repo => repo.DeleteAsync(cartItemId), Times.Once);
        }

        [Fact]
        public async Task Checkout_ShouldClearCartAfterCheckout()
        {
            // Arrange
            var userId = 1;
            var cartItems = TestData.GetCartItems();
            var request = new CheckoutRequest { Ussd = "ussd_code" };

            _mockCartRepository.Setup(repo => repo.GetCartItemsByUserIdAsync(userId)).ReturnsAsync(cartItems);
            _mockCartRepository.Setup(repo => repo.DeleteAsync(It.IsAny<int>())).Returns(Task.CompletedTask);

            // Act
            var result = await _cartService.Checkout(userId, request);

            // Assert
            _mockCartRepository.Verify(repo => repo.DeleteAsync(It.IsAny<int>()), Times.Exactly(cartItems.Count));
            Assert.Contains("Checkout successful using USSD", result);
        }
    }

}