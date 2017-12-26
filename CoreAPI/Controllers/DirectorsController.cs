using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreAPI.Controllers
{
    //via http header
    //e.g. /directors, add api-version header

    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("directors")]
    public class DirectorsController : Controller
    {
        [HttpGet]
        public IActionResult Get() => Content("version1");

        [HttpGet, MapToApiVersion("2.0")]
        public IActionResult GetV2() => Content("version2");
    }
}
