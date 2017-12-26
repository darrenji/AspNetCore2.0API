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
                new Movie{Id=1, LeadActor="a", ReleaseYear=1988, Summary="aaa", Title="aa"},
                new Movie{Id=2, LeadActor="b", ReleaseYear=1989, Summary="bbb", Title="bb"},
                new Movie{Id=3, LeadActor="c", ReleaseYear=1990, Summary="ccc", Title="cc"},
                new Movie{Id=4, LeadActor="d", ReleaseYear=2000, Summary="ddd", Title="dd"},
                new Movie{Id=5, LeadActor="e", ReleaseYear=1950, Summary="eee", Title="ee"},
                new Movie{Id=6, LeadActor="f", ReleaseYear=2017, Summary="fff", Title="ff"}
            };
        }
        public List<Movie> GetMovies(SortingParams sortingParams)
        {
            var query = this.movies.AsQueryable();

            if (!string.IsNullOrEmpty(sortingParams.SortBy))
                query = query.Sort(sortingParams.SortBy);

            return query.ToList();
        }
    }
}
