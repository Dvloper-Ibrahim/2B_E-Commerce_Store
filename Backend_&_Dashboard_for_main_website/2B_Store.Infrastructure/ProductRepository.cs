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

    public class ProductRepository : Repository<Product, int>, IProductRepository
    {
        public ProductRepository(StoreContext dbContext) : base(dbContext) { }

        public async Task<IEnumerable<Product>> GetProductsByCategory(int categoryId)
        {
            return await _Dbset.Where(p => p.SubCategory.CategoryId == categoryId).ToListAsync();
        }

        //public async Task<IEnumerable<Product>> GetProductsBySubCategory(int subCategoryId)
        //{
        //    return await _Dbset.Where(p => p.SubcategoryId == subCategoryId).ToListAsync();
        //}

        //public async Task<IEnumerable<Product>> GetProductsByParentSubCat(int subCategoryId)
        //{
        //    return await _Dbset.Where(p => p.SubCategory.SubcategoryId == null
        //        && p.SubcategoryId == subCategoryId).ToListAsync();
        //}

        public async Task<IEnumerable<Product>> GetProductsByChildSubCat(int subCategoryId)
        {
            //p.SubCategory.SubcategoryId != null &&
            return await _Dbset.Where(p => p.SubcategoryId == subCategoryId).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByBrand(string brand)
        {
            return await _Dbset.Where(p => p.BrandEN == brand || p.BrandAR ==brand).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByPriceRange(decimal minPrice, decimal maxPrice)
        {
            return await _Dbset.Where(p => p.Price >= minPrice && p.Price <= maxPrice).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByStore(string storeName)
        {
           
            return await _Dbset.Where(p => p.LocationStores.Any(ls => ls.NameEN == storeName || ls.NameAR == storeName)).ToListAsync();
        }


        //public async Task<Product> GetProductWithAdditionalInformationById(int productId)
        //{
        //    return await _Dbset.FirstOrDefaultAsync(p => p.Id == productId);
        //}

        //public async Task UpdateProductAdditionalInformation(int productId, string additionalInformation)
        //{
        //    var product = await _Dbset.FindAsync(productId);
        //    if (product != null)
        //    {
        //        product.AdditionalInformation = additionalInformation;
        //        await _dbContext.SaveChangesAsync();
        //    }
        //}


    }
}
