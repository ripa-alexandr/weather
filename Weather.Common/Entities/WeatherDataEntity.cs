
using System;

using Weather.Common.Enums;

namespace Weather.Common.Entities
{
    public class WeatherDataEntity
    {
        public int Id { get; set; }

        public ProviderType Provider { get; set; }

        public string ProviderName { get; set; }

        public DateTime DateTime { get; set; }

        public int CityId { get; set; }

        public virtual CityEntity City { get; set; }

        public virtual WeatherDescriptionEntity WeatherDescription { get; set; }
    }
}
