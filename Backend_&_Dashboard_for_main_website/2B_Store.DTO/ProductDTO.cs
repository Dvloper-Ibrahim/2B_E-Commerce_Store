using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace _2B_Store.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }

        [Display(Name = "Product Name in English")]
        [Required(ErrorMessage = "This field is required")]
        [MinLength(4, ErrorMessage = "Product name must be more than 3 characters")]
        public string ProductNameEN { get; set; }

        [Display(Name = "Product Name in Arabic")]
        [Required(ErrorMessage = "لا بد من ادخال هذا الحقل")]
        [MinLength(4, ErrorMessage = "يجب ان يتكون اسم المنتج من 4 احرف فما اكثر")]
        public string ProductNameAR { get; set; }

        [Display(Name = "Brand in English")]
        [Required(ErrorMessage = "This field is required")]
        public string BrandEN { get; set; }

        [Display(Name = "Brand in Arabic")]
        [Required(ErrorMessage ="لا بد من ادخال هذا الحقل")]
        public string BrandAR { get; set; }

        [Display(Name = "Price in EGP")]
        [Required(ErrorMessage = "This field is required")]
        [Remote("CheckPriceValue", "Product", ErrorMessage = "Price should be greater than 0 EGP")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Remote("CheckStockValue", "Product", ErrorMessage = "Stock quantity should be greater than 0")]
        public int Stock { get; set; }

        [Display(Name = "Description in English")]
        [Required(ErrorMessage = "This field is required")]
        [MinLength(5, ErrorMessage = "Description must be more than 4 characters")]
        public string DescriptionEN { get; set; }

        [Display(Name = "Description in Arabic")]
        [Required(ErrorMessage = "لا بد من ادخال هذا الحقل")]
        [MinLength(5, ErrorMessage = "يجب ان يتكون الوصف من 5 احرف فما اكثر")]
        public string DescriptionAR { get; set; }

        [Display(Name = "Is Available")]
        public bool? IsAvailable { get; set; }

        [Display(Name = "SubCategory")]
        [Required(ErrorMessage = "This field is required")]
        public int SubcategoryId { get; set; }

        public string? Image { get; set; }
        public SubCategoryDTO? SubCategory { get; set; }
        public List<SubCategoryDTO>? SubCategories { get; set; }
        public List<ProductImageDTO>? ProductImages { get; set; }


        //public List<ProductImageDTO> Images { get; set; }
        //public List<ReviewDTO> Reviews { get; set; }
        //public List<LocationStoreDTO> LocationStores { get; set; }
        //public List<ProductAdditionalInfoDTO> AdditionalInformation { get; set; }
        //public List<ProductDetailsDTO> Details { get; set; }




        //public ICollection<ProductImageDTO> Images { get; set; }
        //public ICollection<ReviewDTO> Reviews { get; set; }
        //public ICollection<LocationStoreDTO> LocationStores { get; set; }
        //public Dictionary<string, string> AdditionalInformation { get; set; }
        //public Dictionary<string, string> Details { get; set; }



    }
}
