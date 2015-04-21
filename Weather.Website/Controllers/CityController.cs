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

        public ActionResult Details(CityRequestViewModel request)
        {
            var data = this.weatherFacade.GetAvgWeatherData(request.CityId, request.Date, request.Providers);

            return PartialView("Details", Mapper.Map<IEnumerable<WeatherDataViewModel>>(data));
        }
    }
}