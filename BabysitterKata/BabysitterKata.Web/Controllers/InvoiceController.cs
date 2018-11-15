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
                double startHours = (BedTime - StartTime).TotalHours;
                double bedHours = (EndTime - BedTime).TotalHours;

                result = CalculatePriceFromHours(startHours, bedHours, 0);
            }
            else
            {
                DateTime midnight = new DateTime(StartTime.Year, StartTime.Month, StartTime.Day + 1);
                double startHours = (BedTime - StartTime).TotalHours;
                double bedHours = (midnight - BedTime).TotalHours;
                double endHours = (EndTime - midnight).TotalHours;

                result = CalculatePriceFromHours(startHours, bedHours, endHours);
            }

            return Ok(result);
        }

        private double CalculatePriceFromHours(double InitialHours, double AfterBedHours, double AfterMidnightHours)
        {
            double result;
            //I should put thes somewhere
            double InitialPrice = 12;
            double AfterBedPrice = 8;
            double AfterMidnightPrice = 16;

            result = InitialPrice * InitialHours + AfterBedPrice * AfterBedHours + AfterMidnightPrice * AfterMidnightHours;

            return result;
        }
    }
}