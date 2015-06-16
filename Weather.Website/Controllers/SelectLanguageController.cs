using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Web;
using System.Web.Mvc;

using Weather.Utilities.Extensions;
using Weather.Website.Helpers;
using Weather.Website.Models;
using Weather.Website.Resources;

namespace Weather.Website.Controllers
{
    public class SelectLanguageController : Controller
    {
        public PartialViewResult Index()
        {
            HttpCookie cookie = Request.Cookies["_culture"];

            var viewModel = new SelectLanguageViewModel
            {
                Languages = GetAvailableLanguages(cookie == null ? "" : cookie.Value)
            };

            return PartialView(viewModel);
        }

        [HttpPost]
        public ActionResult SetLanguage(string cultureName, string url)
        {
            HttpCookie cookie = Request.Cookies["_culture"];

            if (cookie != null)
            {
                cookie.Value = cultureName;
            }
            else
            {
                cookie = new HttpCookie("_culture");
                cookie.Value = cultureName;
                cookie.Expires = DateTime.Now.AddYears(1);
            }

            Response.Cookies.Add(cookie);

            return Redirect(url);
        }

        private IEnumerable<SelectListItem> GetAvailableLanguages(string cultureName)
        {
            var rm = new ResourceManager(typeof(AppResource));
            var cultures = CultureHelpers.GetImplementedCultures(rm);
            var selectedCulture = CultureHelpers.GetImplementedCulture(rm, cultureName);
            var result = cultures.Select(i => new SelectListItem
            {
                Value = i.Name,
                Text = rm.GetLocalizeString("Language", i) ?? i.TextInfo.ToTitleCase(i.NativeName),
                Selected = i.Equals(selectedCulture)
            });

            return result;
        }
	}
}