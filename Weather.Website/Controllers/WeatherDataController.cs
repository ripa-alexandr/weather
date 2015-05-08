using System;
using System.Collections.Generic;
using System.Web.Mvc;

using AutoMapper;

using Weather.Website.Models;

namespace Weather.Website.Controllers
{
    public class WeatherDataController : BaseController
    {
        //
        // GET: /WeatherData/
        public ActionResult Index(int cityId)
        {
            var days = WeatherFacade.GetLastSevenDays(DateTime.Now);

            return View(days);
        }

        public ActionResult Details(CityRequestViewModel request)
        {
            var data = WeatherFacade.GetWeatherData(request.CityId, request.Date, request.Providers);

            return PartialView("Details", Mapper.Map<IEnumerable<WeatherDataViewModel>>(data));
        }
	}
}