using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreAPI.Services
{
    public interface IReviewService
    {
        List<Review> GetReviews(int movieId);
        Review GetReview(int movieId, int id);
        void AddReview(Review item);
        void UpdateReview(Review item);
        void DeleteReview(int id);
        bool ReviewExists(int movieId, int id);
    }
}
