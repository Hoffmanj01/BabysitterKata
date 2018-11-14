using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BabysitterKata.Web.Controllers
{
    public class TimeTrackingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}