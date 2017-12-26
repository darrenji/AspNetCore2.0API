using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoreAPI.Service;
using CoreAPI.Models.Reviews;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreAPI.Controllers
{
    [Route("movies/{movieId}/reviews")]
    public class ReviewsController : Controller
    {
        private readonly IReviewService reviewService;
        private readonly IMovieService movieService;

        public ReviewsController(IReviewService reviewService, IMovieService movieService)
        {
            this.reviewService = reviewService;
            this.movieService = movieService;
        }

        [HttpGet]
        public IActionResult Get(int movieId)
        {
            var reviews = reviewService.GetReviews(movieId);
            var outputModel = ToOutputModel(reviews);
            return Ok(outputModel);
        }

        [HttpGet("{id}", Name = "GetReview")]
        public IActionResult Get(int movieId, int id)
        {
            var review = reviewService.GetReview(movieId, id);
            if (review == null)
                return NotFound();

            var outputModel = ToOutputModel(review);
            return Ok(outputModel);
        }

        [HttpPost]
        public IActionResult Create(int movieId, [FromBody]ReviewInputModel inputModel)
        {
            if (inputModel == null)
                return BadRequest();

            if (!movieService.MovieExists(movieId))
                return NotFound();

            var review = ToDomainModel(inputModel);
            reviewService.AddReview(review);

            var outputModel = ToOutputModel(review);
            return CreatedAtRoute("GetReview", new { movieId=outputModel.MovieId, id=outputModel.Id}, outputModel);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int movieId, int id, [FromBody]ReviewInputModel inputModel)
        {
            if (inputModel == null || id != inputModel.Id)
                return BadRequest();

            if (!reviewService.ReviewExists(movieId, id))
                return NotFound();

            var review = ToDomainModel(inputModel);
            reviewService.UpdateReview(review);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int movieId, int id)
        {
            if (!reviewService.ReviewExists(movieId, id))
                return NotFound();

            reviewService.DeleteReview(id);
            return NoContent();
        }

        private ReviewOutputModel ToOutputModel(Review model)
        {
            return new ReviewOutputModel
            {
                Id = model.Id,
                Reviewer = model.Reviewer,
                Comments = model.Comments,
                MovieId = model.MovieId,
                LastReadAt = DateTime.Now
            };
        }

        private List<ReviewOutputModel> ToOutputModel(List<Review> models)
        {
            return models.Select(t => ToOutputModel(t)).ToList();
        }

        private Review ToDomainModel(ReviewInputModel inputModel)
        {
            return new Review
            {
                Id = inputModel.Id,
                Reviewer = inputModel.Reviewer,
                Comments = inputModel.Comments,
                MovieId = inputModel.MovieId
            };
        }
    }
}
