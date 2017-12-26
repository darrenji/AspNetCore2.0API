using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoreAPI.Services;
using CoreAPI.Models;
using CoreAPI.Lib;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreAPI.Controllers
{
    [Route("movies")]
    public class MoviesController : Controller
    {
        private readonly IMovieService service;

        public MoviesController(IMovieService service)
        {
            this.service = service;
        }

        [HttpGet(Name = "GetMovies")]
        public IActionResult Get(SortingParams sortingParams)
        {
            var model = service.GetMovies(sortingParams);
            var outputModel = new MovieOutputModel
            {
                Items = model.Select(m => ToMovieInfo(m)).ToList()
            };
            return Ok(outputModel);
        }

        private MovieInfo ToMovieInfo(Movie model)
        {
            return new MovieInfo
            {
                Id = model.Id,
                Title = model.Title,
                ReleaseYear = model.ReleaseYear,
                Summary = model.Summary,
                LeadActor = model.LeadActor,
                LastReadAt = DateTime.Now
            };
        }
    }

   
}
