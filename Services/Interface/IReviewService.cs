using Models;
using Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface IReviewService
    {
        public Task<List<GetReviewResponse>> GetReviewsAsync();
        public Task<Review> GetReviewByIdAsync(int id);
        public Task<Review> CreateReviewAsync(CreateReviewRequest request);
        public Task<bool> DeleteReviewAsync(int id);     
        public Task<Review> UpdateReviewAsync(int id, CreateReviewRequest request);
        public Task<Review> GetReviewByBookingIdAsync(int id);
    }
}
