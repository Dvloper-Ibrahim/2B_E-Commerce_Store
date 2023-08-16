
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace _2B_Store.Application.Contracts
{

    public interface IReviewRepository : IRepository<Review, int>
    {
       
        Task<IEnumerable<Review>> GetReviewsByProductId(int productId);
        Task<IEnumerable<Review>> GetReviewsByUserId(string userId);
    }



}
