using _2B_Store.Application.Contracts;
using _2B_Store.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2B_Store.Infrastructure
{
    public class ProductImageRepository : Repository<ProductImage, int>, IProductImageRepository
    {
        public ProductImageRepository(StoreContext dbContext) : base(dbContext) { }

        public async Task<IEnumerable<ProductImage>> GetImagesByProductId(int productId)
        {
            return await _Dbset.Where(image => image.ProductId == productId).ToListAsync();
        }

        public async Task<ProductImage> GetImageByProductData(int productId, string productImg)
        {
            return await _Dbset.FirstOrDefaultAsync(image => image.ProductId == productId &&
                image.ImageUrl == productImg);
        }
    }
}
