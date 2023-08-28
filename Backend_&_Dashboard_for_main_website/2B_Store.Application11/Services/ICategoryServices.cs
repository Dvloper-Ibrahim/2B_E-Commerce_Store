using _2B_Store.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2B_Store.Application.Services
{
    public interface ICategoryServices
    {
        Task<List<CategoryDTO>> GetAllCategories();
        Task<CategoryDTO> GetCategoryById(int categoryId);
        Task<CategoryDTO> AddCategory(CategoryDTO categoryDTO);
        Task<CategoryDTO> UpdateCategory(int categoryId, CategoryDTO categoryDTO);
        Task DeleteCategory(int categoryId);



        /////////////////////////////////////////////////////////////////////////////////////////
        //// public Task<List<GetAllCategsDTO>> GetCategoryallPagination(int Item, int pagenumber);
        // public Task<CategoryDTO> GetCategoryById(int Categoryid);
        // ////public Task<List<Category>> FindCategories(string searchTerm, int? minSubCategories, int? maxSubCategories);
        // ////public Category GetCategoryByName(Category name);
        // public Task<Create_updateCategDTO> AddCategory(Create_updateCategDTO categoryDTO);
        // public Task<Create_updateCategDTO> UpdateCategory(int categoryId, Create_updateCategDTO categoryDTO);


    }
}
