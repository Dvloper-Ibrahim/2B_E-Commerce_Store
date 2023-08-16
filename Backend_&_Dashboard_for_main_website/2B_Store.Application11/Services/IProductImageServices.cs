using _2B_Store.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2B_Store.Application11.Services
{
    public interface IProductImageServices
    {
        Task<IEnumerable<ProductImageDTO>> GetImagesByProductId(int productId);
        Task<ProductImageDTO> AddProductImage(ProductImageDTO productImageDTO);
        Task DeleteProductImage(int imageId);


    }
}
