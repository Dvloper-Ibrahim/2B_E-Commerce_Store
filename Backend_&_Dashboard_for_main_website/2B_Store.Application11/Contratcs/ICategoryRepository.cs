using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace _2B_Store.Application.Contracts
{

    public interface ICategoryRepository : IRepository<Category, int>
    {
      //  Task<Category> GetCategoryById(int categoryId);
    }

}
