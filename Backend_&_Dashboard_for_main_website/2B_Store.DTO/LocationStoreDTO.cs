using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2B_Store.DTO
{
    public class LocationStoreDTO
    {
        public int Id { get; set; }
        public string NameEN { get; set; }
        public string NameAR { get; set; }

        public string StreetEN { get; set; }
        public string StreetAR { get; set; }

        public string CityEN { get; set; }
        public string CityAR { get; set; }

        public string CountryEN { get; set; }
        public string CountryAR { get; set; }

        public string ImageStore { get; set; }
        public int Tel_Number { get; set; }

        public virtual ICollection<ProductDTO> Products { get; set; }
    }
}
