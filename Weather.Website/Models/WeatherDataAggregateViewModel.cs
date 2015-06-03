
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Weather.Common.Enums;
using Weather.Website.Resources;

namespace Weather.Website.Models
{
    public class WeatherDataAggregateViewModel
    {
        public IEnumerable<Provider> Providers { get; set; }

        public DateTime DateTime { get; set; }

        [Display(Name = "WeatherData_TimeOfDay", ResourceType = typeof(AppResource))]
        public string TimeOfDay { get; set; }

        [Display(Name = "WeatherData_Cloudy", ResourceType = typeof(AppResource))]
        public string Cloudy { get; set; }

        [Display(Name = "WeatherData_Precipitation", ResourceType = typeof(AppResource))]
        public string Precipitation { get; set; }

        [Display(Name = "WeatherData_AirTemp", ResourceType = typeof(AppResource))]
        public double AirTemp { get; set; }

        [Display(Name = "WeatherData_RealFeel", ResourceType = typeof(AppResource))]
        public double? RealFeel { get; set; }

        [Display(Name = "WeatherData_Pressure", ResourceType = typeof(AppResource))]
        public double Pressure { get; set; }

        [Display(Name = "WeatherData_WindDirection", ResourceType = typeof(AppResource))]
        public string WindDirection { get; set; }

        [Display(Name = "WeatherData_WindSpeed", ResourceType = typeof(AppResource))]
        public double WindSpeed { get; set; }

        [Display(Name = "WeatherData_Humidity", ResourceType = typeof(AppResource))]
        public double Humidity { get; set; }

        [Display(Name = "WeatherData_ChancePrecipitation", ResourceType = typeof(AppResource))]
        public double? ChancePrecipitation { get; set; }
    }
}