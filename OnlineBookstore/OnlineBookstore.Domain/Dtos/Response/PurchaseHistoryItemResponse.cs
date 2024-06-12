using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookstore.Domain.Dtos.Response
{
    public class PurchaseHistoryItemResponse
    {
        public int Id { get; set; }
        public int PurchaseHistoryId { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public BookResponse Book { get; set; }
    }

}
