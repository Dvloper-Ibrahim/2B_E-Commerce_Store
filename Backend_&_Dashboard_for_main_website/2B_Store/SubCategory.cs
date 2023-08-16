using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2B_Store
{
    public class SubCategory
    {
        public int Id { get; set; }
        public string NameEN { get; set; }
        public string NameAR { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public int? SubcategoryId { get; set; }
        public virtual SubCategory Subcategory { get; set; }
        /// <summary>
        ///Each  SubCategory can have multiple child SubCategory entities associated with it.
        ///represented by the ICollection<SubCategory> SubCategories property in the SubCategory entity.
        /// 
        /// </summary>
        public virtual ICollection<SubCategory> SubCategories { get; set; }

        public virtual ICollection<Product> Products { get; set; }

    }
}
