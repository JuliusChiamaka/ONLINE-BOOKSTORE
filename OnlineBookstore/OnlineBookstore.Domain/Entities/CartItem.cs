using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookstore.Domain.Entities
{
    public class CartItem
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public DateTime AddedAt { get; set; }

        //// Navigation properties
        //public AppUser User { get; set; }
        //public Book Book { get; set; }
    }


}
