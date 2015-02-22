
using AutoMapper;

using Weather.Common.Entities;

namespace Weather.Bootstrap
{
    public static class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.CreateMap<WeatherDataEntity, WeatherDataEntity>()
                .ForMember(d => d.Id, opt => opt.Ignore())
                .ForMember(d => d.City, opt => opt.Ignore());

            Mapper.CreateMap<WeatherDescriptionEntity, WeatherDescriptionEntity>()
                .ForMember(d => d.Id, opt => opt.Ignore())
                .ForMember(d => d.WeatherData, opt => opt.Ignore());
        }
    }
}
