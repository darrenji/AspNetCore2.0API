using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreAPI.Lib
{
    public class BaseController : Controller
    {
        [NonAction]
        public UnprocessableObjectResult Unprocessable(ModelStateDictionary modelState)
        {
            return new UnprocessableObjectResult(modelState);
        }

        [NonAction]
        public ObjectResult Unprocessable(object value)
        {
            return new UnprocessableObjectResult(value);
        }

        public IActionResult OkOrNotFound(object model)
        {
            return (model == null) ? NotFound() : (IActionResult)Ok(model);
        }
    }
}
