using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Weather.Website.Controllers
{
    public class WeatherController : Controller
    {
        //
        // GET: /Weather/
        public ActionResult Index(int cityId)
        {
            return View();
        }
	}
}