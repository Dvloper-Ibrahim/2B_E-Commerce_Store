using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2B_Store.Application.Contracts
{
    public interface ISubCategoryRepository : IRepository<SubCategory, int>
    {
        //  Task<SubCategory> GetSubCategoryById(int subCategoryId);
        Task<IEnumerable<SubCategory>> GetSubCategoriesByParentSubCat(int subCategoryId);
    }
}
