using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2B_Store.DTO
{
    public class GetAllProdsDTO
    {
        public int Id { get; set; }
        public string ProductNameEN { get; set; }
        public string ProductNameAR { get; set; }

        public decimal Price { get; set; }
        public int Stock { get; set; }


        public int SubcategoryId { get; set; }
        public virtual SubCategory SubCategory { get; set; }
        public virtual ICollection<ProductImage> Images { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<LocationStore> LocationStores { get; set; }
    }
}
