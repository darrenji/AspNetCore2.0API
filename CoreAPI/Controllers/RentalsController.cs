using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoreAPI.Models;
using CoreAPI.Lib;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreAPI.Controllers
{
    [Route("")]
    public class RentalsController : BaseController
    {
        [HttpPost("request-rental", Name = "RequestRental")]
        public IActionResult RequestRental([FromBody]RequestRentalInputModel inputModel)
        {
            return AcceptedAtRoute("CheckStatus", new { queueNo = "a-2" });
        }

        [HttpGet("check-status/{queueNo}", Name = "CheckStatus")]
        public IActionResult CheckStatus(string queueNo)
        {
            if (queueNo == "q-2")
                return Ok(new CheckStatusOutputModel { Status = "Pending" });
            else
                return SeeOther("GetRental", new { refNo = "r-1"});
        }

        [HttpGet("get-rental/{refNo}", Name ="GetRental")]
        public IActionResult GetRental(string refNo)
        {
            if (refNo == "r-1")
                return Ok(new GetRentalOutputModel { DeliveryEstimate = DateTime.Now.AddDays(2) });
            else
                return NotFound();
        }
    }
}
