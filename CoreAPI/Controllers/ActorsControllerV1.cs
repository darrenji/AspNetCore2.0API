using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreAPI.Controllers
{
    // via url
    // e.g. actors/v2.0

    [ApiVersion("1.0")]
    [Route("actors/v{ver:apiVersion}")]
    public class ActorsControllerV1 : Controller
    {
        [HttpGet]
        public IActionResult Get() => Content("version1");
    }

    [ApiVersion("2.0")]
    [Route("actors/v{ver:apiVersion}")]
    public class ActorsControllerV2 : Controller
    {
        [HttpGet]
        public IActionResult Get() => Content("version2");
    }
}
