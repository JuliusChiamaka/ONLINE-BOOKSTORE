using AutoMapper;
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
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public CartService(ICartRepository cartRepository, IBookRepository bookRepository, IMapper mapper)
        {
            _cartRepository = cartRepository;
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CartItemResponse>> GetCartItemsByUserIdAsync(int userId)
        {
            var cartItems = await _cartRepository.GetCartItemsByUserIdAsync(userId);
            return _mapper.Map<IEnumerable<CartItemResponse>>(cartItems);
        }

        public async Task AddToCartAsync(AddToCartRequest request)
        {
            var book = await _bookRepository.GetByIdAsync(request.BookId);
            if (book == null)
            {
                throw new Exception("Book not found");
            }

            var cartItem = _mapper.Map<CartItem>(request);
            await _cartRepository.AddAsync(cartItem);
        }

        public async Task RemoveFromCartAsync(int cartItemId)
        {
            await _cartRepository.DeleteAsync(cartItemId);
        }

        public async Task<string> Checkout(int userId, CheckoutRequest request)
        {
            var cartItems = await _cartRepository.GetCartItemsByUserIdAsync(userId);
            if (cartItems == null || !cartItems.Any())
            {
                throw new Exception("Cart is empty");
            }

            var totalAmount = 0m;

            foreach (var item in cartItems)
            {
                var book = await _bookRepository.GetByIdAsync(item.BookId);
                if (book == null)
                {
                    throw new Exception($"Book with ID {item.BookId} not found");
                }
                totalAmount += item.Quantity * book.Price;
            }

            var paymentMethod = string.Empty;
            if (!string.IsNullOrEmpty(request.Ussd))
            {
                paymentMethod = "USSD";
            }
            else if (!string.IsNullOrEmpty(request.Web))
            {
                paymentMethod = "Web";
            }
            else if (!string.IsNullOrEmpty(request.Transfer))
            {
                paymentMethod = "Transfer";
            }
            else
            {
                throw new Exception("No payment method provided");
            }

            // Simulate the checkout process
            // Here I would normally integrate with a payment gateway

            // Clear the cart after successful checkout
            foreach (var item in cartItems)
            {
                await _cartRepository.DeleteAsync(item.Id);
            }

            return $"Checkout successful using {paymentMethod}. Total amount: {totalAmount:C}";
        }


    }
}
