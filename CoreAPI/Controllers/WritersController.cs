using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreAPI.Controllers
{
    // conventions specified in Startup
    // via HTTP header
    //e.g. /writers,  add api-version header

    [Route("writers")]
    public class WritersControllerV1 : Controller
    {
        [HttpGet]
        public IActionResult Get() => Content("Version1");
    }

    [Route("writers")]
    public class WritersControllerV2 : Controller
    {
        [HttpGet]
        public IActionResult Get() => Content("Version2");
    }
}
