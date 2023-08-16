using _2B_Store.Application.Contracts;
using _2B_Store.DTO;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2B_Store.Application11.Services
{
    public class ReviewServices : IReviewServices
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;

        public ReviewServices(IReviewRepository reviewRepository, IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
        }

        public async Task<List<ReviewDTO>> GetReviewsByProductId(int productId)
        {
            var reviews = await _reviewRepository.GetReviewsByProductId(productId);
            return _mapper.Map<List<ReviewDTO>>(reviews);
        }

        public async Task<List<ReviewDTO>> GetReviewsByUserId(string userId)
        {
            var reviews = await _reviewRepository.GetReviewsByUserId(userId);
            return _mapper.Map<List<ReviewDTO>>(reviews);
        }

        public async Task<ReviewDTO> AddReview(ReviewDTO reviewDTO)
        {
            var review = _mapper.Map<Review>(reviewDTO);
            review = await _reviewRepository.AddAsync(review);
            await _reviewRepository.SaveChangesAsync();
            return _mapper.Map<ReviewDTO>(review);
        }

        public async Task<ReviewDTO> UpdateReview(int reviewId, ReviewDTO reviewDTO)
        {
            var existingReview = await _reviewRepository.GetByIdAsync(reviewId);
            if (existingReview == null)
                throw new ArgumentException("Review not found");

            _mapper.Map(reviewDTO, existingReview);
            existingReview = await _reviewRepository.UpdateAsync(existingReview);
            await _reviewRepository.SaveChangesAsync();
            return _mapper.Map<ReviewDTO>(existingReview);
        }

        public async Task DeleteReview(int reviewId)
        {
            var existingReview = await _reviewRepository.GetByIdAsync(reviewId);
            if (existingReview == null)
                throw new ArgumentException("Review not found");

            await _reviewRepository.DeleteAsync(existingReview);
            await _reviewRepository.SaveChangesAsync();
        }
    
    }
}
