using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookstore.Domain.Dtos.Request
{
    public class PurchaseHistoryDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal TotalAmount { get; set; }
        public List<PurchaseHistoryItemDto> Items { get; set; } = new List<PurchaseHistoryItemDto>();
    }
}
