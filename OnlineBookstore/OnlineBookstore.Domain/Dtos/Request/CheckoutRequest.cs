using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookstore.Domain.Dtos.Request
{
    public class CheckoutRequest
    {
        public string Ussd { get; set; }
        public string Web { get; set; }
        public string Transfer { get; set; }
    }
}
