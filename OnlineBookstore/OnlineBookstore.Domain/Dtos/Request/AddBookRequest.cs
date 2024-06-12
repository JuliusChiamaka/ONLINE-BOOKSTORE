using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookstore.Domain.Dtos.Request
{
    public class AddBookRequest
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public string ISBN { get; set; }
        public string Author { get; set; }
        public int PublicationYear { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
    }
}
