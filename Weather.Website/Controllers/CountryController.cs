using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AutoMapper;

using Weather.Website.Models;

namespace Weather.Website.Controllers
{
    public class CountryController : BaseController
    {
        //
        // GET: /Country/
        public ActionResult Index(int worldId)
        {
            var countries = WeatherFacade.GetCountries(worldId);

            return View(Mapper.Map<IEnumerable<CountryViewModel>>(countries));
        }
	}
}