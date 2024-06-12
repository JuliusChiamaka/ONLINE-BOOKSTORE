using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookstore.Domain.Dtos.Response
{
    public class BookResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string ISBN { get; set; }
        public string Author { get; set; }
        public int PublicationYear { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
    }
}
