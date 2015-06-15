using System;
using System.Globalization;
using System.Resources;
using System.Threading;
using System.Web;
using System.Web.Mvc;

using Ninject;

using Weather.Bootstrap;
using Weather.Facade;
using Weather.Website.Helpers;
using Weather.Website.Resources;

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

        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            string cultureName = null;

            HttpCookie cultureCookie = Request.Cookies["_culture"];

            if (cultureCookie != null)
            {
                cultureName = cultureCookie.Value;
            }
            else
            {
                cultureName = Request.UserLanguages != null && Request.UserLanguages.Length > 0 ? Request.UserLanguages[0] : null;
            }

            var rm = new ResourceManager(typeof(AppResource));
            cultureName = CultureHelpers.GetImplementedCulture(rm, cultureName).Name;

            Thread.CurrentThread.CurrentCulture = new CultureInfo(cultureName);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
            
            return base.BeginExecuteCore(callback, state);
        }
    }
}