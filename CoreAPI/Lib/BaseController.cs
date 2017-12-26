using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreAPI.Lib
{
    public class BaseController : Controller
    {
        [NonAction]
        protected IActionResult SeeOther(string routeName, object values)
        {
            var location = Url.Link(routeName, values);
            HttpContext.Response.GetTypedHeaders().Location = new System.Uri(location);
            return StatusCode(StatusCodes.Status303SeeOther);
        }
    }
}
