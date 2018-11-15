using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BabysitterKata.Web.Controllers
{
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        [Route("[controller]/GetInvoicePrice")]
        [HttpGet]
        public IActionResult GetInvoicePrice(DateTime StartTime, DateTime BedTime, DateTime EndTime)
        {
            //The plan is to make the most money while not over exagurating the hours due to the change in hourly wages
            double result = 0;  //due to this being currency I am preferring to use a double over an int

            if (EndTime.Day == StartTime.Day)
            {
                double startHours = ()
            }
            else
            {

            }

            return Ok(result);
        }
    }
}