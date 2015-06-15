using System.Collections.Generic;
using System.Web.Mvc;

namespace Weather.Website.Models
{
    public class SelectLanguageViewModel
    {
        public IEnumerable<SelectListItem> Languages { get; set; }
    }
}