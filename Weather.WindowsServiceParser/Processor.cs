
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using Ninject;

using NLog;

using Weather.Bootstrap;
using Weather.Common.Entities;
using Weather.Common.Exceptions;
using Weather.Common.Message.Request;
using Weather.DAL.Repository;
using Weather.Parser;

namespace Weather.WindowsServiceParser
{
    public class Processor
    {
        private readonly LinkRepository linkRepository;
        private readonly WeatherDataRepository weatherDataRepository;
        private readonly SinoptikProvider sinoptikProvider;
        private readonly GismeteoProvider gismeteoProvider;
        private readonly Rp5Provider rp5Provider;
        private readonly Logger logger;

        public Processor(Logger logger)
        {
            var kernel = Kernel.Initialize();

            this.linkRepository = kernel.Get<LinkRepository>();
            this.weatherDataRepository = kernel.Get<WeatherDataRepository>();
            this.sinoptikProvider = kernel.Get<SinoptikProvider>();
            this.gismeteoProvider = kernel.Get<GismeteoProvider>();
            this.rp5Provider = kernel.Get<Rp5Provider>();
            this.logger = logger;

            AutoMapperConfiguration.Configure();
        }

        public void Process()
        {
            try
            {
                var stopWatch = new Stopwatch();
                stopWatch.Start();

                this.ProcessWeatherData();
                
                stopWatch.Stop();
                this.logger.Info("Parse end, spend {0:g} for parsing", stopWatch.Elapsed);
            }
            catch (NotImplementedMethodException ex)
            {
                this.logger.Error(ex.Message);
                this.logger.Error(ex.MethodArgs);
                this.logger.Error(ex.StackTrace);
            }
            catch (Exception ex)
            {
                this.logger.Error(ex.Message);
                this.logger.Error(ex.StackTrace);
            }
        }

        private void ProcessWeatherData()
        {
            var links = this.linkRepository.Get().ToList();
            var weatherData = links.AsParallel().SelectMany(this.ProcessLink).ToList();
            this.weatherDataRepository.AddOrUpdate(weatherData);
            
            this.logger.Info("Save data to the database");
            this.weatherDataRepository.Save();
        }

        private IEnumerable<WeatherData> ProcessLink(Link link)
        {
            var result = Enumerable.Empty<WeatherData>();

            switch (link.TypeProvider)
            {
                case TypeProvider.Gismeteo:
                    result = this.gismeteoProvider.Fetch(new ProviderRequest { Url = link.Url }).WeatherData;
                    break;

                case TypeProvider.Sinoptik:
                    result = this.sinoptikProvider.Fetch(new ProviderRequest { Url = link.Url }).WeatherData;
                    break;

                case TypeProvider.Rp5:
                    result = this.rp5Provider.Fetch(new ProviderRequest { Url = link.Url }).WeatherData;
                    break;
            }

            foreach (var item in result)
            {
                item.CityId = link.CityId;
            }

            return result;
        }
    }
}
