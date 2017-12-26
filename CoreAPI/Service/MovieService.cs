using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreAPI.Service
{
    public class MovieService : IMovieService
    {
        private readonly List<Movie> movies;

        public MovieService()
        {
            this.movies = new List<Movie> {
                new Movie{Id=1, Title="never say never again", ReleaseYear=1983, Summary="a spectrue agent has stolen two american"},
                new Movie{Id=2, Title="diamonds are orever", ReleaseYear=1971, Summary="a diamond smuggling investigation leads"},
                new Movie{Id=3, Title="you only live tweice", ReleaseYear=1967, Summary="agent 007 and the secret servi e"}
            };
        }
        public void AddMovie(Movie item)
        {
            this.movies.Add(item);
        }

        public void DeleteMovie(int id)
        {
            var movie = this.movies.Where(t => t.Id == id).FirstOrDefault();
            this.movies.Remove(movie);
        }

        public Movie GetMovie(int id)
        {
            return this.movies.Where(t => t.Id == id).FirstOrDefault();
        }

        public List<Movie> GetMovies()
        {
            return this.movies.ToList();
        }

        public bool MovieExists(int id)
        {
            return this.movies.Any(t => t.Id == id);
        }

        public void UpdateMovie(Movie item)
        {
            var movie = this.movies.Where(t => t.Id == item.Id).FirstOrDefault();
            movie.Title = item.Title;
            movie.ReleaseYear = item.ReleaseYear;
            movie.Summary = item.Summary;
        }
    }
}
