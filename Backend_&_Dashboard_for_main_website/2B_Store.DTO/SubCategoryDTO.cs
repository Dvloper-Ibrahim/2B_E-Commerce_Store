using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace _2B_Store.DTO
{
    public class SubCategoryDTO
    {
        public int Id { get; set; }

        [Display(Name = "SubCategory Name in English")]
        [Required(ErrorMessage = "This field is required")]
        [MinLength(2, ErrorMessage = "SubCategory name must be more than 1 characters")]
        public string NameEN { get; set; }

        [Display(Name = "SubCategory Name in Arabic")]
        [Required(ErrorMessage = "لا بد من ادخال هذا الحقل")]
        [MinLength(2, ErrorMessage = "يجب ان يتكون الاسم من 3 احرف فما اكثر")]
        public string NameAR { get; set; }

        [Display(Name = "Category")]
        //[Required(ErrorMessage = "This field is required")]
        public int CategoryId { get; set; }
        public virtual CategoryDTO? Category { get; set; }

        [Display(Name = "SubCategory")]
        public int? SubcategoryId { get; set; }
        public virtual SubCategoryDTO? Subcategory { get; set; }

        public virtual ICollection<CategoryDTO>? Categories { get; set; }
        public virtual ICollection<SubCategoryDTO>? SubCategories { get; set; }
        //public virtual ICollection<Product> Products { get; set; }
    }
}
