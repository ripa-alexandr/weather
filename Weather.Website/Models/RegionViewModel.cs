using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Weather.Website.Models
{
    public class RegionViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int CountryId { get; set; }

        public CountryViewModel Country { get; set; }

        public IEnumerable<CityViewModel> Cities { get; set; }
    }
}