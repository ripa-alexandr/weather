using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AutoMapper;

using Weather.Website.Models;

namespace Weather.Website.Controllers
{
    public class RegionController : BaseController
    {
        //
        // GET: /Region/
        public ActionResult Index(int countryId)
        {
            var regiones = WeatherFacade.GetRegiones(countryId);

            return View(Mapper.Map<IEnumerable<RegionViewModel>>(regiones));
        }
	}
}