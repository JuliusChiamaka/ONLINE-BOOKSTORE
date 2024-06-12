using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookstore.Domain.Entities
{
    public class PurchaseHistoryItem
    {
        public int Id { get; set; }
        public int PurchaseHistoryId { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        // Navigation properties
        //public PurchaseHistory PurchaseHistory { get; set; }
        //public Book Book { get; set; }
    }

}
