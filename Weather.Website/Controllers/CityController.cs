using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web.Mvc;

using AutoMapper;

using Weather.Common.Enums;
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

        public ActionResult Details(int cityId, DateTime? date, IEnumerable<Provider> providers)
        {
            // TODO: DateTime.Now and defaultProviders?
            var result = GetDetails(cityId, date ?? DateTime.Now, providers ?? WebsiteConfig.DefaultProviders);

            if (Request.IsAjaxRequest())
                return PartialView("WeatherData", result.WeatherData);

            return View(result);
        }

        private CityViewModel GetDetails(int cityId, DateTime date, IEnumerable<Provider> providers)
        {
            var result = new CityViewModel
            {
                Id = cityId,
                LastSevenDays = WeatherFacade.GetLastSevenDays(cityId, date, providers),
                WeatherData = Mapper.Map<IEnumerable<WeatherDataAggregateViewModel>>(WeatherFacade.GetWeatherData(cityId, date, providers))
            };

            return result;
        }
    }
}