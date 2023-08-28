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
    public class SubCategoryRepository : Repository<SubCategory, int>, ISubCategoryRepository
    {
        public SubCategoryRepository(StoreContext dbContext) : base(dbContext) { }

        public async Task<IEnumerable<SubCategory>> GetSubCategoriesByParentSubCat(int subCategoryId)
        {
            return await _Dbset.Where(sc => sc.SubcategoryId == subCategoryId).ToListAsync();
        }
    }
}
