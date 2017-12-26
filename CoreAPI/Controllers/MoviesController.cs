using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoreAPI.Lib;
using CoreAPI.Service;
using CoreAPI.Models.Movies;
using Microsoft.AspNetCore.JsonPatch;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreAPI.Controllers
{
    [Route("movies")]
    public class MoviesController : BaseController
    {
        private readonly IMovieService service;

        public MoviesController(IMovieService service)
        {
            this.service = service;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var movies = service.GetMovies();
            var outputModel = ToOutputModel(movies);
            return Ok(outputModel);

        }

        [HttpGet("{id}", Name = "GetMovie")]
        public IActionResult Get(int id)
        {
            var movie = service.GetMovie(id);
            if (movie == null)
                return NotFound();
            var outputModel = ToOutputModel(movie);
            return Ok(outputModel);
        }

        [HttpPost]
        public IActionResult Create([FromBody]MovieInputModel inputModel)
        {
            if (inputModel == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return Unprocessable(ModelState);

            var movie = ToDomainModel(inputModel);
            service.AddMovie(movie);

            var outputModel = ToOutputModel(movie);
            return CreatedAtRoute("GetMovie", new { id = outputModel.Id }, outputModel);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]MovieInputModel inputModel)
        {
            if (inputModel == null || id != inputModel.Id)
                return BadRequest();

            if (!service.MovieExists(id))
                return NotFound();

            if (!ModelState.IsValid)
                return new UnprocessableObjectResult(ModelState);

            var movie = ToDomainModel(inputModel);
            service.UpdateMovie(movie);

            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult UpdatePatch(int id, [FromBody]JsonPatchDocument<MovieInputModel> patch)
        {
            if (patch == null)
                return BadRequest();

            var movie = service.GetMovie(id);
            if (movie == null)
                return NotFound();

            var inputModel = ToInputModel(movie);
            patch.ApplyTo(inputModel);

            TryValidateModel(inputModel);
            if (!ModelState.IsValid)
                return new UnprocessableObjectResult(ModelState);

            movie = ToDomainModel(inputModel);
            service.UpdateMovie(movie);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!service.MovieExists(id))
                return NotFound();

            service.DeleteMovie(id);
            return NoContent();
        }


        private MovieOutputModel ToOutputModel(Movie model)
        {
            return new MovieOutputModel
            {
                Id = model.Id,
                Title = model.Title,
                ReleaseYear = model.ReleaseYear,
                Summary = model.Summary,
                LastReadAt = DateTime.Now
            };
        }

        private List<MovieOutputModel> ToOutputModel(List<Movie> models)
        {
            return models.Select(t => ToOutputModel(t)).ToList();
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

        private MovieInputModel ToInputModel(Movie model)
        {
            return new MovieInputModel
            {
                Id = model.Id,
                Title = model.Title,
                ReleaseYear = model.ReleaseYear,
                Summary = model.Summary
            };
        }
    }
}
