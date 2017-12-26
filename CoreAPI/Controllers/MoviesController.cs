using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoreAPI.Models;
using Microsoft.AspNetCore.JsonPatch;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreAPI.Controllers
{
    [Route("movies")]
    public class MoviesController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            var outputModel = new List<MovieOutputModel>();
            return Ok(outputModel);
        }

        [HttpGet("{id}", Name = "GetMovie")]
        public IActionResult Get(int id)
        {
            var outputModel = new MovieOutputModel();
            return Ok(outputModel);
        }

        [HttpPost]
        [ProducesResponseType(typeof(MovieOutputModel), 201)]
        [Produces("application/json",Type = typeof(MovieOutputModel))]
        public IActionResult Create([FromBody]MovieInputModel inputModel)
        {
            var outputModel = new MovieOutputModel();
            return CreatedAtRoute("GetMovie",new { id=outputModel.Id}, outputModel);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]MovieInputModel inputModel)
        {
            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult UpdatePatch(int id, [FromBody]JsonPatchDocument<MovieInputModel> patch)
        {
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return NoContent();
        }
    }
}
