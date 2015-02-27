
using System;

using Weather.Common.Enums;
using Weather.Common.Interfaces;

namespace Weather.Common.Entities
{
    public class WeatherDataEntity : IBaseEntity
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
