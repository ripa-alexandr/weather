using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Ninject;

using Weather.Bootstrap;
using Weather.DAL.Repository.Interface;
using Weather.Facade;

namespace Weather.Website.Controllers
{
    public abstract class BaseController : Controller
    {
        protected readonly IWeatherFacade WeatherFacade;

        protected BaseController()
        {
            var kernel = Kernel.Initialize();

            this.WeatherFacade = kernel.Get<IWeatherFacade>();
        }
    }
}