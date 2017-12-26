using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoreAPI.Service;
using CoreAPI.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreAPI.Controllers
{
    [Route("movies")]
    public class MoviesController : Controller
    {
        private readonly IMovieService movieService;

        public MoviesController(IMovieService movieService)
        {
            this.movieService = movieService;
        }

        [HttpGet()]
        [HttpGet("/movies.{format}"), FormatFilter]
        public IActionResult Get()
        {
            var model = movieService.GetMovies();
            var outputModel = ToOutputModel(model);
            return Ok(outputModel);
        }

        [HttpGet("{id}", Name = "GetMovie")]
        public IActionResult Get(int id)
        {
            var model = movieService.GetMovie(id);
            var outputModel = ToOutputModel(model);
            return Ok(outputModel);
        }

        [HttpPost]
        public IActionResult Create([FromBody]MovieInputModel inputModel)
        {
            var model = ToDomainModel(inputModel);
            movieService.AddMovie(model);

            var outputModel = ToOutputModel(model);
            return CreatedAtRoute("GetMovie", new { id=outputModel.Id}, outputModel);
        }

        private MovieOutputModel ToOutputModel(Movie model)
        {
            if (model == null) return null;
            return new MovieOutputModel
            {
                Id = model.Id,
                Title = model.Title,
                ReleaseYear = model.ReleaseYear,
                Summary = model.Summary,
                LastReadAt = DateTime.Now
            };
        }

        private List<MovieOutputModel> ToOutputModel(List<Movie> model)
        {
            return model.Select(item => ToOutputModel(item)).ToList();
        }

        private Movie ToDomainModel(MovieInputModel inputModel)
        {
            return new Movie
            {
                Id = inputModel.Id,
                Title = inputModel.Title,
                ReleaseYear = inputModel.ReleaseYear,
                Summary = inputModel.Summary
            };
        }
    }
}
