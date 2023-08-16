namespace _2B_Store.DTO
{
    public class CreateUpdateProductDTO
    {
        public string ProductNameEN { get; set; } 
        public string ProductNameAR { get; set; } 
        public string BrandEN { get; set; }
        public string BrandAR { get; set; }

        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string DescriptionEN { get; set; }
        public string DescriptionAR { get; set; }

        public bool IsAvailable { get; set; }

        public int SubcategoryId { get; set; }

        public List<ProductImageDTO> Images { get; set; }
        public List<ProductAdditionalInfoDTO> AdditionalInformation { get; set; }
        public List<ProductDetailsDTO> Details { get; set; }



        //public Dictionary<string, string> AdditionalInformation { get; set; }
        //public Dictionary<string, string> Details { get; set; }
    }
}