using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreAPI.Lib;

namespace CoreAPI.Services
{
    public class MovieService : IMovieService
    {
        private readonly List<Movie> movies;

        public MovieService()
        {
            this.movies = new List<Movie>
            {
                new Movie{Id=1, LeadActor="d", Title="heelo", ReleaseYear=1982, Summary="good"},
                new Movie{Id=2, LeadActor="a", Title="a", ReleaseYear=1982, Summary="aaa"},
                new Movie{Id=3, LeadActor="c", Title="c", ReleaseYear=1982, Summary="ccc"},
                new Movie{Id=4, LeadActor="d", Title="d", ReleaseYear=1982, Summary="ddd"},
                new Movie{Id=5, LeadActor="e", Title="ee", ReleaseYear=1982, Summary="eee"},
                new Movie{Id=6, LeadActor="f", Title="ff", ReleaseYear=1982, Summary="fff"}
            };
        }
        public List<Movie> GetMovies(FilteringParams filteringParams)
        {
            var query = this.movies.AsQueryable();

            var filterBy = filteringParams.FilterBy.Trim().ToLowerInvariant();
            if(!string.IsNullOrEmpty(filterBy))
            {
                query = query
                    .Where(t => t.LeadActor.ToLowerInvariant().Contains(filterBy)
                    || t.Title.ToLowerInvariant().Contains(filterBy)
                    || t.Summary.ToLowerInvariant().Contains(filterBy));
            }
            return query.ToList();
        }
    }
}
