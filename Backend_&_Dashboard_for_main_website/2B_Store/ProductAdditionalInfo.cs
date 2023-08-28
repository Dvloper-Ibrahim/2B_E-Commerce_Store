using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2B_Store
{
    public class ProductAdditionalInfo
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string ValueEN { get; set; } 
        public string ValueAR { get; set; } 

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}

//Now, you have updated all the relevant layers to include the ProductDetails and ProductAdditionalInfo entities in the Product class. Make sure to perform migrations and update the database to apply these changes successfully.




