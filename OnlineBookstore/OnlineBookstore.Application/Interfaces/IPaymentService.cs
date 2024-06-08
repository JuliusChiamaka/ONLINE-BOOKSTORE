using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookstore.Application.Interfaces
{
    public interface IPaymentService
    {
        Task<string> Checkout(int userId, string paymentMethod);
    }
}
