﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoreAPI.Lib;
using CoreAPI.Services;
using CoreAPI.Models.Movies;
using Microsoft.AspNetCore.JsonPatch;

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
            var model = service.GetMovies();
            var outputModel = ToOutputModel(model);
            return Ok(outputModel);
        }

        [HttpGet("{id}", Name = "GetMovie")]
        public IActionResult Get(int id)
        {
            var model = service.GetMovie(id);
            if (model == null)
                return NotFound();

            var outputModel = ToOutputModel(model);
            return Ok(outputModel);
        }

        [HttpPost]
        public IActionResult Create([FromBody]MovieInputModel inputModel)
        {
            if (inputModel == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return Unprocessable(ModelState);

            var model = ToDomainModel(inputModel);
            service.AddMovie(model);

            var outputModel = ToOutputModel(model);
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

            var model = ToDomainModel(inputModel);
            service.UpdateMovie(model);

            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult UpdatePatch(int id, [FromBody]JsonPatchDocument<MovieInputModel> patch)
        {
            if (patch == null)
                return BadRequest();

            var model = service.GetMovie(id);
            if (model == null)
                return NotFound();

            var inputModel = ToInputModel(model);
            patch.ApplyTo(inputModel);

            TryValidateModel(inputModel);
            if (!ModelState.IsValid)
                return new UnprocessableObjectResult(ModelState);

            model = ToDomainModel(inputModel);
            service.UpdateMovie(model);
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

        private MovieOutputModel ToOutputModel(Movie movie)
        {
            return new MovieOutputModel
            {
                Id = movie.Id,
                Title = movie.Title,
                ReleaseYear = movie.ReleaseYear,
                Summary = movie.Summary,
                LastReadAt = DateTime.Now
            };
        }

        private List<MovieOutputModel> ToOutputModel(List<Movie> movies)
        {
            return movies.Select(t => ToOutputModel(t)).ToList();
        }
    }
}