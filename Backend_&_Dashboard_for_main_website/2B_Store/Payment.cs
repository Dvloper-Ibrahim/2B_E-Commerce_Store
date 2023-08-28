using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2B_Store
{
    public class Payment
    {
        public int Id { get; set; }
        public string PaymentMethod { get; set; }
        public string TransactionId { get; set; }
       

        public int OrderId { get; set; }
        public virtual Order Order { get; set; }

    }
}
