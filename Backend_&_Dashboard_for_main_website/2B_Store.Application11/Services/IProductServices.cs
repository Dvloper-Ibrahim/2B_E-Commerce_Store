using _2B_Store.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2B_Store.Application.Services
{
 
    public interface IProductServices
    {
        Task<List<ProductDTO>> GetProductAllPagination(int itemsPerPage, int pageNumber);
        Task<List<ProductDTO>> GetAllProducts();
        Task<ProductDTO> GetProductById(int productId);
        Task<ProductDTO> AddProduct(ProductDTO productDTO);
        Task<ProductDTO> UpdateProduct(int productId, ProductDTO productDTO);
        Task DeleteProduct(int productId);
        Task<List<ProductDTO>> GetProductsByCategory(int categoryId);
        //Task<List<ProductDTO>> GetProductsBySubCategory(int subCategoryId);
        Task<List<ProductDTO>> GetProductsByParentSubCat(int subCategoryId);
        Task<List<ProductDTO>> GetProductsByChildSubCat(int subCategoryId);
        Task<List<ProductDTO>> GetProductsByBrand(string brand);
        Task<List<ProductDTO>> GetProductsByPriceRange(decimal minPrice, decimal maxPrice);
        Task<List<ProductDTO>> GetProductsByStore(string storeName);
        Task<List<ProductDTO>> SearchProducts(string query);
    }
}



//public Task<List<GetAllProdsDTO>> GetProductallPagination(int Item, int pagenumber);
//public Task<GetAllProdsDTO> GetProductById(int Categoryid);

//public Task<Create_updateProdDTO> AddProduct(Create_updateProdDTO productDTO);
//public Task<Create_updateProdDTO> UpdateProduct(int categoryId, Create_updateProdDTO productDTO);

//public Task<CategoryDTO> DeleteProduct(int categoryId);