using System;
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
        private readonly IUrlHelper urlHelper;

        public MoviesController(IMovieService service, IUrlHelper urlHelper)
        {
            this.service = service;
            this.urlHelper = urlHelper;
        }

        [HttpGet(Name = "GetMovies")]
        public IActionResult Get(PagingParams pagingParams)
        {
            var model = service.GetMovies(pagingParams);
            Response.Headers.Add("X-Pagination", model.GetHeader().ToJson());

            var outputModel = new MovieOutputModel
            {
                Paging = model.GetHeader(),
                Links = GetLinks(model),
                Items = model.List.Select(t => ToMovieInfo(t)).ToList()
            };
            return Ok(outputModel);
        }


        private List<LinkInfo> GetLinks(PagedList<Movie> list)
        {
            var links = new List<LinkInfo>();

            if(list.HasPreviousPage)
            {
                links.Add(CreateLink("GetMovies", list.PreviousPageNumber, list.PageSize, "previousPage", "GET"));
            }
            else
            {
                links.Add(CreateLink("GetMovies", list.PageNumber, list.PageSize, "self", "GET"));
            }

            if(list.HasNextPage)
            {
                links.Add(CreateLink("GetMovies", list.NextPageNumber, list.PageSize, "nextPage", "GET"));
            }

            return links;
        }

        private LinkInfo CreateLink(string routeName, int pageNumber, int pageSize, string rel, string method)
        {
            return new LinkInfo
            {
                Href = urlHelper.Link(routeName, new { PageNumber = pageNumber, PageSize = pageSize }),
                Rel = rel,
                Method = method
            };
        }

        private MovieInfo ToMovieInfo(Movie model)
        {
            return new MovieInfo
            {
                Id = model.Id,
                Title = model.Title,
                ReleaseYear = model.ReleaseYear,
                Summary = model.Summary,
                LastReadAt = DateTime.Now
            };
        }
    }
}