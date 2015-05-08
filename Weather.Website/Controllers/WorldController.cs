using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using AutoMapper;

using Weather.Website.Models;

namespace Weather.Website.Controllers
{
    public class WorldController : BaseController
    {
        //
        // GET: /World/
        public ActionResult Index()
        {
            var worlds = WeatherFacade.GetWorlds();
            
            return View(Mapper.Map<IEnumerable<WorldViewModel>>(worlds));
        }
	}
}