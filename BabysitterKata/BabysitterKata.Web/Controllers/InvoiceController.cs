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
            //The plan is to make the most money while not over exagurating the hours due to the change in hourly wages (however rounding up to the nearest hour
            double result = 0;  //due to this being currency I am preferring to use a double over an int

            if (EndTime.Day == StartTime.Day)
            {
                double startHours = (BedTime - StartTime).TotalHours;
                double bedHours = (EndTime - BedTime).TotalHours;

                int[] calcHours = ConvertToFullHours(startHours, bedHours, 0);

                result = CalculatePriceFromHours(calcHours[0], calcHours[1], calcHours[2]);
            }
            else
            {
                DateTime midnight = new DateTime(StartTime.Year, StartTime.Month, StartTime.Day + 1);
                double startHours = (BedTime - StartTime).TotalHours;
                double bedHours = (midnight - BedTime).TotalHours;
                //zero out the negitive if the kids never go to bed
                if (bedHours < 0) bedHours = 0; 
                double endHours = (EndTime - midnight).TotalHours;

                int[] calcHours = ConvertToFullHours(startHours, bedHours, endHours);

                result = CalculatePriceFromHours(calcHours[0], calcHours[1], calcHours[2]);
            }

            return Ok(result);
        }

        private double CalculatePriceFromHours(int InitialHours, int AfterBedHours, int AfterMidnightHours)
        {
            double result;
            //I should put thes somewhere
            double InitialPrice = 12;
            double AfterBedPrice = 8;
            double AfterMidnightPrice = 16;

            result = InitialPrice * InitialHours + AfterBedPrice * AfterBedHours + AfterMidnightPrice * AfterMidnightHours;

            return result;
        }

        internal int[] ConvertToFullHours(double StartHours, double BedHours, double EndHours)
        {
            int[] results = new int[] { (int)StartHours, (int)BedHours, (int)EndHours };

            if (EndHours % 1 != 0)
            {
                int endResult = (int)Math.Ceiling(EndHours);
                double diff = endResult - EndHours;

                if (diff <= BedHours) BedHours = BedHours - diff;
                else StartHours = StartHours - diff;

                results[2] = endResult;
            }

            if (StartHours % 1 != 0)
            {
                int startResult = (int)Math.Ceiling(StartHours);
                double diff = startResult - StartHours;

                BedHours = BedHours - diff;

                results[0] = startResult;
            }

            if (BedHours % 1 != 0)
            {
                results[1] = (int)Math.Ceiling(BedHours);
            }

            return results;
        }
    }
}