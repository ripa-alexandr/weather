
using System.Collections.Generic;

namespace Weather.Website.Models
{
    public class CityViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<string> LastSevenDays { get; set; }

        public IEnumerable<WeatherDataViewModel> WeatherData { get; set; }
    }
}