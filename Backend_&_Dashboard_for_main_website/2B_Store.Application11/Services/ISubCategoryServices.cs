using _2B_Store.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2B_Store.Application11.Services
{
    public interface ISubCategoryServices
    {
        Task<List<SubCategoryDTO>> GetAllSubCategories();
        Task<SubCategoryDTO> GetSubCategoryById(int subCategoryId);
        Task<SubCategoryDTO> AddSubCategory(SubCategoryDTO subCategoryDTO);
        Task<SubCategoryDTO> UpdateSubCategory(int subCategoryId, SubCategoryDTO subCategoryDTO);
        Task DeleteSubCategory(int subCategoryId);
        Task<List<SubCategoryDTO>> GetParentSubCategsBy_CategId(int categoryId);
        Task<List<SubCategoryDTO>> GetChildSubCatby_ParentSubId(int subCatId);
    }
}
