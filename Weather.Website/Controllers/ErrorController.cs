using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Weather.Website.Controllers
{
    public class ErrorController : Controller
    {
        public ViewResult Error404()
        {
            return View();
        }

        public ViewResult Error500()
        {
            return View();
        }
	}
}