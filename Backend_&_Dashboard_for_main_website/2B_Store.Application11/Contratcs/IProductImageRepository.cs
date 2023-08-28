using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2B_Store.Application.Contracts
{
    public interface IProductImageRepository : IRepository<ProductImage, int>
    {
        Task<IEnumerable<ProductImage>> GetImagesByProductId(int productId);
        Task<ProductImage> GetImageByProductData(int productId, string productImg);
    }
}
