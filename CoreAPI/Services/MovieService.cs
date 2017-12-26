using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreAPI.Services
{
    public class MovieService : IMovieService
    {
        private readonly List<Movie> movies;

        public MovieService()
        {
            this.movies = new List<Movie>
            {
                new Movie{Id=1, Title="never say never again", ReleaseYear=1983, Summary="a pecture agent"},
                new Movie{Id=2, Title="diamonds are forever", ReleaseYear=1971, Summary="a diamond smuggling"},
                new Movie{Id=3, Title="you only live twice", ReleaseYear=1967, Summary="agent 007 and the"},
                new Movie{Id=4, Title="you only live twice4", ReleaseYear=1967, Summary="agent 007 and the"},
                new Movie{Id=5, Title="you only live twice5", ReleaseYear=1967, Summary="agent 007 and the"},
                new Movie{Id=6, Title="you only live twice6", ReleaseYear=1967, Summary="agent 007 and the"},
                new Movie{Id=7, Title="you only live twice7", ReleaseYear=1967, Summary="agent 007 and the"},
                new Movie{Id=8, Title="you only live twice8", ReleaseYear=1967, Summary="agent 007 and the"},
                new Movie{Id=9, Title="you only live twice9", ReleaseYear=1967, Summary="agent 007 and the"},
                new Movie{Id=10, Title="you only live twice10", ReleaseYear=1967, Summary="agent 007 and the"},
                new Movie{Id=11, Title="you only live twice11", ReleaseYear=1967, Summary="agent 007 and the"}
            };
        }

        public PagedList<Movie> GetMovies(PagingParams pagingParams)
        {
            var query = this.movies.AsQueryable();
            return new PagedList<Movie>(query, pagingParams.PageNumber, pagingParams.PageSize);
        }
    }
}
