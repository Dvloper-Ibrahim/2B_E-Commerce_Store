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
    public class ReviewRepository : Repository< Review , int> , IReviewRepository
    {
        public ReviewRepository (StoreContext dbContext) : base(dbContext) { }

     
        public async Task<IEnumerable<Review>> GetReviewsByProductId(int productId)
        {
            return await _Dbset.Where(r => r.ProductId == productId).ToListAsync();
        }

        public async Task<IEnumerable<Review>> GetReviewsByUserId(string userId)
        {
            return await _Dbset.Where(r => r.UserId == userId).ToListAsync();
        }
    }
}
