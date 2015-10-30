
using AutoMapper;

using Weather.Common.Dto;
using Weather.DAL.Entities;

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
        }

        private static void ConfigureEntityToDto()
        {
            Mapper.CreateMap<CityEntity, CityDto>()
                .ForMember(d => d.Region, opt => opt.Ignore())
                .ForMember(d => d.WeatherData, opt => opt.Ignore())
                .ForMember(d => d.Links, opt => opt.Ignore());

            Mapper.CreateMap<CountryEntity, CountryDto>()
                .ForMember(d => d.World, opt => opt.Ignore())
                .ForMember(d => d.Regions, opt => opt.Ignore());

            Mapper.CreateMap<LinkEntity, LinkDto>()
                .ForMember(d => d.City, opt => opt.Ignore());

            Mapper.CreateMap<RegionEntity, RegionDto>()
                .ForMember(d => d.Country, opt => opt.Ignore())
                .ForMember(d => d.Cities, opt => opt.Ignore());

            Mapper.CreateMap<WeatherDataEntity, WeatherDataDto>()
                .ForMember(d => d.City, opt => opt.Ignore());

            Mapper.CreateMap<WorldEntity, WorldDto>()
                .ForMember(d => d.Countries, opt => opt.Ignore());
        }

        private static void ConfigureDtoToEntity()
        {
            Mapper.CreateMap<CityDto, CityEntity>()
                .ForMember(d => d.Region, opt => opt.Ignore())
                .ForMember(d => d.WeatherData, opt => opt.Ignore())
                .ForMember(d => d.Links, opt => opt.Ignore());

            Mapper.CreateMap<CountryDto, CountryEntity>()
                .ForMember(d => d.World, opt => opt.Ignore())
                .ForMember(d => d.Regions, opt => opt.Ignore());

            Mapper.CreateMap<LinkDto, LinkEntity>()
                .ForMember(d => d.City, opt => opt.Ignore());

            Mapper.CreateMap<RegionDto, RegionEntity>()
                .ForMember(d => d.Country, opt => opt.Ignore())
                .ForMember(d => d.Cities, opt => opt.Ignore());

            Mapper.CreateMap<WeatherDataDto, WeatherDataEntity>()
                .ForMember(d => d.City, opt => opt.Ignore());

            Mapper.CreateMap<WorldDto, WorldEntity>()
                .ForMember(d => d.Countries, opt => opt.Ignore());
        }
    }
}
