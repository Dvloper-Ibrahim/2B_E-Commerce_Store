using _2B_Store.Application.Contracts;
using _2B_Store.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2B_Store.Infrastructure
{
    public class CategoryRepository : Repository<Category, int>, ICategoryRepository
    {
        public CategoryRepository(StoreContext dbContext) : base(dbContext) { }

    }
}
