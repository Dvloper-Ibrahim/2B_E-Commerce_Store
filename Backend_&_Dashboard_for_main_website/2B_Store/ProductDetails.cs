using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2B_Store
{
    public class ProductDetails
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string ValueEN { get; set; } 
        public string ValueAR { get; set; } 

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
