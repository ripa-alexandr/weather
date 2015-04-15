using System;
using System.Collections.Generic;
using System.Web.Mvc;

using Antlr.Runtime;

using AutoMapper;

using Ninject;

using Weather.Bootstrap;
using Weather.Facade;
using Weather.Website.Models;

namespace Weather.Website.Controllers
{
    public class CityController : Controller
    {
        private readonly IWeatherFacade weatherFacade;

        public CityController()
        {
            var kernel = Kernel.Initialize();

            this.weatherFacade = kernel.Get<IWeatherFacade>();
        }

        public ActionResult Index()
        {
            return View();
        }

        //public ActionResult Details(int cityId, DateTime date, int[] providers)
        //{
        //    var data = this.weatherFacade.GetAvgWeatherData(cityId, date, (IEnumerable<int>)providers);

        //    return PartialView("Details", Mapper.Map<WeatherDataViewModel>(data));
        //}

        [HttpPost]
        public ActionResult Details(CityRequestViewModel request)
        {
            var data = this.weatherFacade.GetAvgWeatherData(request.CityId, request.Date, request.Providers);

            return PartialView("Details", Mapper.Map<WeatherDataViewModel>(data));
        }
    }
}