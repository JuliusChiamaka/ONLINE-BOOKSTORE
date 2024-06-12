using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookstore.Domain.Dtos.Request
{
    public class AddPurchaseHistoryRequest
    {
        public int UserId { get; set; }
        public decimal TotalAmount { get; set; }
        public List<AddPurchaseHistoryItemRequest> Items { get; set; } = new List<AddPurchaseHistoryItemRequest>();
    }
}
