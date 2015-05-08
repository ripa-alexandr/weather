using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Weather.Website.Models
{
    public class CountryViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int WorldId { get; set; }

        public WorldViewModel World { get; set; }

        public IEnumerable<RegionViewModel> Regions { get; set; }
    }
}