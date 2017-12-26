﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreAPI.Services
{
    public interface IMovieService
    {
        PagedList<Movie> GetMovies(PagingParams pagingParams);
    }
}
