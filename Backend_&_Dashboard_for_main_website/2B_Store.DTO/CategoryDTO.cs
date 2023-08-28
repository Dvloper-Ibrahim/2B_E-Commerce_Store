using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace _2B_Store.DTO
{
    public class CategoryDTO
    {
        public int Id { get; set; }

        [Display(Name = "Category Name in English")]
        [Required(ErrorMessage = "This field is required")]
        [MinLength(4, ErrorMessage = "Category name must be more than 3 characters")]
        public string NameEN { get; set; }

        [Display(Name = "Category Name in Arabic")]
        [Required(ErrorMessage = "لا بد من ادخال هذا الحقل")]
        [MinLength(4, ErrorMessage = "يجب ان يتكون الاسم من 4 احرف فما اكثر")]
        public string NameAR { get; set; }

        [MinLength(4, ErrorMessage = "Type must be more than 3 characters")]
        public string Type { get; set; }

        public string? Image { get; set; }

        [Display(Name = "Description in English")]
        [Required(ErrorMessage = "This field is required")]
        [MinLength(5, ErrorMessage = "Description must be more than 4 characters")]
        public string DescriptionEN { get; set; }

        [Display(Name = "Description in Arabic")]
        [Required(ErrorMessage = "لا بد من ادخال هذا الحقل")]
        [MinLength(5, ErrorMessage = "يجب ان يتكون الوصف من 5 احرف فما اكثر")]
        public string DescriptionAR { get; set; }

        //public virtual ICollection<SubCategory> SubCategories { get; set; }
    
    }
}
