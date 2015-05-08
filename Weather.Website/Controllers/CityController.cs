using System.Collections.Generic;
using System.Web.Mvc;

using AutoMapper;

using Weather.Website.Models;

namespace Weather.Website.Controllers
{
    public class CityController : BaseController
    {
        public ActionResult Index(int regionId)
        {
            var cities = WeatherFacade.GetCities(regionId);

            return View(Mapper.Map<IEnumerable<CityViewModel>>(cities));
        }
    }
}