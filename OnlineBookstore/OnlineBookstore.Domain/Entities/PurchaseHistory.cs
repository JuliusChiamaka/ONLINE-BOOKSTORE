using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookstore.Domain.Entities
{
    public class PurchaseHistory
    {
        public int Id { get; set; } 
        public int UserId { get; set; }
        public int BooksId { get; set; }
        public DateTime PurchaseDate { get; set; }

    }
}
