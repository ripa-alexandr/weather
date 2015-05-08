using System;
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

        public ActionResult Details(int cityId)
        {
            var result = new CityViewModel
            {
                Id = cityId,
                LastSevenDays = WeatherFacade.GetLastSevenDays(DateTime.Now),
            };

            return View(result);
        }

        public ActionResult GetWeatherData(CityRequestViewModel request)
        {
            var data = WeatherFacade.GetWeatherData(request.CityId, request.Date, request.Providers);

            return PartialView("WeatherData", Mapper.Map<IEnumerable<WeatherDataViewModel>>(data));
        }
    }
}