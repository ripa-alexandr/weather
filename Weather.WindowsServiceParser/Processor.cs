
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using Ninject;

using NLog;

using Weather.Bootstrap;
using Weather.DAL.Repository;
using Weather.Data.Entities;
using Weather.Data.Exceptions;
using Weather.Parser;

namespace Weather.WindowsServiceParser
{
    public class Processor
    {
        private readonly CityRepository repository;
        private readonly SinoptikProvider sinoptikProvider;
        private readonly GismeteoProvider gismeteoProvider;
        private readonly Rp5Provider rp5Provider;
        private readonly Logger logger;

        public Processor(Logger logger)
        {
            var kernel = Kernel.Initialize();

            this.repository = kernel.Get<CityRepository>();
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
            var cities = this.repository.Get().Include(i => i.Links);

            Parallel.ForEach(cities, this.ProcessCity);
            
            this.logger.Info("Save data to the database");
            this.repository.Save();
        }

        private void ProcessCity(City city)
        {
            var weatherData = city.Links.AsParallel().SelectMany(this.ProcessLink).ToArray();

            var lockObj = new object();

            lock (lockObj)
            {
                this.repository.AddOrUpdateWeatherData(city, weatherData);
            }
        }

        private IEnumerable<WeatherData> ProcessLink(Link link)
        {
            switch (link.TypeProvider)
            {
                case TypeProvider.Gismeteo:
                    return this.gismeteoProvider.Fetch(link.Url);
                    
                case TypeProvider.Sinoptik:
                    return this.sinoptikProvider.Fetch(link.Url);
                    
                case TypeProvider.Rp5:
                    return this.rp5Provider.Fetch(link.Url);
            }

            throw new NotImplementedException();
        }
    }
}
