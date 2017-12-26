using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoreAPI.Models;
using CoreAPI.Lib;
using Microsoft.AspNetCore.Http;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreAPI.Controllers
{
    [Route("movies")]
    public class MoviesController : Controller
    {
        const string ETAG_HEADER = "ETag";
        const string MATCH_HEADER = "If-Match";

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var model_from_db = new Movie
            {
                Id = 1,
                Title = "title1",
                ReleaseYear = 1965
            };

            //每次获取到一个实体就hash，然后放到ETag中
            var eTag = HashFactory.GetHash(model_from_db);
            HttpContext.Response.Headers.Add(ETAG_HEADER, eTag);

            if (HttpContext.Request.Headers.ContainsKey(MATCH_HEADER) && HttpContext.Request.Headers[MATCH_HEADER].RemoveQuotes() == eTag)
                return new StatusCodeResult(StatusCodes.Status304NotModified);

            return Ok(model_from_db);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Movie model)
        {
            var model_from_db = new Movie
            {
                Id = 1,
                Title = "title1-changed",
                ReleaseYear = 1965
            };

            var eTag = HashFactory.GetHash(model_from_db);
            HttpContext.Response.Headers.Add(ETAG_HEADER, eTag);

            if(!HttpContext.Request.Headers.ContainsKey(MATCH_HEADER) || HttpContext.Request.Headers[MATCH_HEADER].RemoveQuotes()!=eTag)
            {
                return new StatusCodeResult(StatusCodes.Status412PreconditionFailed);
            }
            else
            {
                //saving should be ok
            }
            return NoContent();
        }
    }
}
