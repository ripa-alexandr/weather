
using AutoMapper;

using Weather.Common.Dto;
using Weather.Common.Entities;

namespace Weather.Bootstrap
{
    public static class AutoMapperConfiguration
    {
        public static void Configure()
        {
            ConfigureCommon();
            ConfigureEntityToDto();
            ConfigureDtoToEntity();
        }

        private static void ConfigureCommon()
        {
            Mapper.CreateMap<WeatherDataEntity, WeatherDataEntity>()
                .ForMember(d => d.Id, opt => opt.Ignore())
                .ForMember(d => d.City, opt => opt.Ignore());

            Mapper.CreateMap<WeatherDescriptionEntity, WeatherDescriptionEntity>()
                .ForMember(d => d.Id, opt => opt.Ignore())
                .ForMember(d => d.WeatherData, opt => opt.Ignore());
        }

        private static void ConfigureEntityToDto()
        {
            Mapper.CreateMap<CityEntity, CityDto>();
            Mapper.CreateMap<CountryEntity, CountryDto>();
            Mapper.CreateMap<LinkEntity, LinkDto>();
            Mapper.CreateMap<RegionEntity, RegionDto>();
            Mapper.CreateMap<WeatherDataEntity, WeatherDataDto>();
            Mapper.CreateMap<WeatherDescriptionEntity, WeatherDescriptionDto>();
            Mapper.CreateMap<WorldEntity, WorldDto>();
        }

        private static void ConfigureDtoToEntity()
        {
            Mapper.CreateMap<CityDto, CityEntity>();
            Mapper.CreateMap<CountryDto, CountryEntity>();
            Mapper.CreateMap<LinkDto, LinkEntity>();
            Mapper.CreateMap<RegionDto, RegionEntity>();
            Mapper.CreateMap<WeatherDataDto, WeatherDataEntity>();
            Mapper.CreateMap<WeatherDescriptionDto, WeatherDescriptionEntity>();
            Mapper.CreateMap<WorldDto, WorldEntity>();
        }
    }
}
