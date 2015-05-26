
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Weather.Website.Models
{
    public class CityViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<SelectListItem> Providers
        {
            get
            {
                var all = WebsiteConfig.DefaultProviders;
                var selected = WeatherData.SelectMany(i => i.Providers).Distinct();
                var result = all.Select(p => new SelectListItem
                {
                    Text = p.ToString(), 
                    Value = ((int)p).ToString(), 
                    Selected = selected.Any(sp => sp == p)
                });

                return result;
            }
        }

        public IEnumerable<string> LastSevenDays { get; set; }

        public IEnumerable<WeatherDataAggregateViewModel> WeatherData { get; set; }
    }
}