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
    public class PaymentService : IPaymentService
    {
        private readonly IPurchaseHistoryRepository _historyRepository;
        private readonly IShoppingCartRepository _cartRepository;

        public PaymentService(IPurchaseHistoryRepository historyRepository, IShoppingCartRepository cartRepository)
        {
            _historyRepository = historyRepository;
            _cartRepository = cartRepository;
        }

        public async Task<string> Checkout(int userId, string paymentMethod)
        {
            var cartItems = await _cartRepository.GetCartByUserIdAsync(userId);
            if (cartItems == null )
            {
                throw new Exception("No items in the cart.");
            }

           
            await Task.Delay(500);

            string paymentResult;
            switch (paymentMethod.ToLower())
            {
                case "web":
                    paymentResult = "Payment successful via Web.";
                    break;
                case "ussd":
                    paymentResult = "Payment successful via USSD.";
                    break;
                case "transfer":
                    paymentResult = "Payment successful via Transfer.";
                    break;
                default:
                    throw new ArgumentException("Invalid payment method.");
            }

            foreach (var item in cartItems)
            {
                var history = new PurchaseHistory
                {
                    UserId = item.UserId,
                    BookId = item.BookId,
                    PurchaseDate = DateTime.UtcNow
                };

                await _historyRepository.AddPurchaseHistoryAsync(history);
                await _cartRepository.RemoveFromCartAsync(item.Id);
            }

            return paymentResult;
        }
    }
}
