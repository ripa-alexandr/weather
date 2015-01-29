using AutoMapper;

using Weather.Data.Entities;

namespace Weather.Bootstrap
{
    public static class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.CreateMap<WeatherData, WeatherData>()
                .ForMember(d => d.Id, opt => opt.Ignore());

            Mapper.CreateMap<WeatherDescription, WeatherDescription>()
                .ForMember(d => d.Id, opt => opt.Ignore())
                .ForMember(d => d.WeatherData, opt => opt.Ignore());
        }
    }
}
