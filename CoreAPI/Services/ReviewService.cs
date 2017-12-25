using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreAPI.Services
{
    public class ReviewService : IReviewService
    {
        private readonly List<Review> reviews;

        public ReviewService()
        {
            this.reviews = new List<Review>
            {
                new Review{Id=1, MovieId=1, Reviewer="darren", Comments="good"},
                new Review{Id=2, MovieId=1, Reviewer="jack", Comments="excellent"},
                new Review{Id=3, MovieId=2, Reviewer="sunny",Comments="very good"},
                new Review{Id=4, MovieId=3, Reviewer="bob", Comments="average"},
                new Review{Id=5, MovieId=3, Reviewer="smith",Comments="interesting"}
            };
        }
        public void AddReview(Review item)
        {
            this.reviews.Add(item);
        }

        public void DeleteReview(int id)
        {
            var model = this.reviews.Where(t => t.Id == id).FirstOrDefault();
            this.reviews.Remove(model);
        }

        public Review GetReview(int movieId, int id)
        {
            return this.reviews.Where(t => t.MovieId == movieId && t.Id == id).FirstOrDefault();
        }

        public List<Review> GetReviews(int movieId)
        {
            return this.reviews.Where(t => t.MovieId == movieId).ToList();
        }

        public bool ReviewExists(int movieId, int id)
        {
            return this.reviews.Any(t => t.MovieId == movieId && t.Id == id);
        }

        public void UpdateReview(Review item)
        {
            var model = this.reviews.Where(t => t.Id == item.Id).FirstOrDefault();

            model.Reviewer = item.Reviewer;
            model.Comments = item.Comments;
        }
    }
}
