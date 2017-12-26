﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreAPI.Controllers
{
    // via query string
    // e.g. /movies?api-version=2.0

    [ApiVersion("1.0")]
    [Route("movies")]
    public class MoviesControllerV1:Controller
    {
        [HttpGet]
        public IActionResult Get() => Content("movie version1");
    }

    //这里没有成功
    [ApiVersion("2.0")]
    [Route("movies")]
    public class MoviesControllerV2 : Controller
    {
        [HttpGet]
        public IActionResult Get() => Content("movie version2");
    }
}
