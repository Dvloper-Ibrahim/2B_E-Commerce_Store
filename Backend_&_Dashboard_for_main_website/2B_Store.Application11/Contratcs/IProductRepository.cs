using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace _2B_Store.Application.Contracts
{
    public interface IProductRepository : IRepository<Product, int>
    {
       // Task<Product> GetProductById(int productId);
        Task<IEnumerable<Product>> GetProductsByCategory(int categoryId);
        //Task<IEnumerable<Product>> GetProductsBySubCategory(int subCategoryId);
        //Task<IEnumerable<Product>> GetProductsByParentSubCat(int subCategoryId);
        Task<IEnumerable<Product>> GetProductsByChildSubCat(int subCategoryId);
        Task<IEnumerable<Product>> GetProductsByBrand(string brand);
        Task<IEnumerable<Product>> GetProductsByPriceRange(decimal minPrice, decimal maxPrice);
        Task<IEnumerable<Product>> GetProductsByStore(string storeName);
    }
}
