﻿
using System;

using Weather.Common.Enums;

namespace Weather.Common.Dto
{
    public class WeatherDataDto : WeatherDataBaseDto
    {
        public int Id { get; set; }

        public DateTime DateTime { get; set; }

        public Provider Provider { get; set; }

        public string ProviderName { get; set; }

        public int CityId { get; set; }

        public CityDto City { get; set; }
    }
}
