using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreAPI.Controllers
{
    //via http header
    //e.g. /genres add api-version header

    [ApiVersion("1.0", Deprecated = true)]
    [Route("genres")]
    public class GenresControllerV1 : Controller
    {
        public IActionResult Get() => Content("version1");
    }

    [ApiVersion("2.0")]
    [Route("genres")]
    public class GenresControllerV2 : Controller
    {
        [HttpGet]
        public IActionResult Get() => Content("version2");
    }
}
