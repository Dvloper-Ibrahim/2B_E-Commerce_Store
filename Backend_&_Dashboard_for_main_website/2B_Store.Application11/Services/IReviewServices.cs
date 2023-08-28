using _2B_Store.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2B_Store.Application11.Services
{
    public interface IReviewServices
    {
        Task<List<ReviewDTO>> GetReviewsByProductId(int productId);
        Task<List<ReviewDTO>> GetReviewsByUserId(string userId);
        Task<ReviewDTO> AddReview(ReviewDTO reviewDTO);
        Task<ReviewDTO> UpdateReview(int reviewId, ReviewDTO reviewDTO);
        Task DeleteReview(int reviewId);

    }
}
