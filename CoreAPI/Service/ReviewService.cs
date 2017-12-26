using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreAPI.Service
{
    public class ReviewService : IReviewService
    {
        private readonly List<Review> reviews;

        public ReviewService()
        {
            this.reviews = new List<Review> {
                new Review{Id=1, MovieId=1, Reviewer="smith",Comments="good"},
                new Review{Id=2, MovieId=1, Reviewer="jessie",Comments="excellent"},
                new Review{Id=3, MovieId=2, Reviewer="bog", Comments="fantastic"},
                new Review{Id=4, MovieId=3, Reviewer="tech",Comments="not bad"},
                new Review{Id=5, MovieId=3, Reviewer="sk", Comments="very good"}
            };
        }
        public void AddReview(Review item)
        {
            this.reviews.Add(item);
        }

        public void DeleteReview(int id)
        {
            var review = this.reviews.Where(t => t.Id == id).FirstOrDefault();
            this.reviews.Remove(review);
        }

        public Review GetReview(int movieId, int id)
        {
            return this.reviews.Where(t => t.Id == id && t.MovieId == movieId).FirstOrDefault();
        }

        public List<Review> GetReviews(int movieId)
        {
            return this.reviews.Where(t => t.MovieId == movieId).ToList();
        }

        public bool ReviewExists(int movieId, int id)
        {
            return this.reviews.Any(t => t.Id == id && t.MovieId == movieId);
        }

        public void UpdateReview(Review item)
        {
            var model = this.reviews.Where(t => t.Id == item.Id).FirstOrDefault();
            model.Reviewer = item.Reviewer;
            model.Comments = item.Comments;

        }
    }
}
