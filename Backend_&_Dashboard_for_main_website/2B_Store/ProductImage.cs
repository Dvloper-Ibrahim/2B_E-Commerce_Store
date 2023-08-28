using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2B_Store
{
    public class ProductImage
    {
        public int Id { get; set; }
        //[RegularExpression(@"\w+\.(jpg|png|gif)")]
        public string ImageUrl { get; set; }
       

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
