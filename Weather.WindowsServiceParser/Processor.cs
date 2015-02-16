
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using Ninject;

using NLog;

using Weather.Bootstrap;
using Weather.Common.Entities;
using Weather.Common.Exceptions;
using Weather.Common.Extensions;
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
            var stopWatch = new Stopwatch();

            try
            {
                stopWatch.Start();

                this.ProcessWeatherData();

                stopWatch.Stop();
                this.logger.Info("Parse end, spend {0:g} for parsing", stopWatch.Elapsed);
            }
            catch (AggregateException ae)
            {
                stopWatch.Stop();
                this.logger.Info("Parse end, spend {0:g} for parsing", stopWatch.Elapsed);
                this.HandleException(ae);
            }
        }

        private void HandleException(AggregateException ae)
        {
            foreach (var ex in ae.InnerExceptions)
            {
                var nime = ex as NotImplementedMethodException;

                if (nime != null)
                {
                    var msg = "TypeException: {0}\r\nMessage: {1}\r\nMethodArgs: {2}\r\nStackTrace: {3}\r\n".F(nime.GetType().Name, nime.Message, nime.MethodArgs, nime.StackTrace);
                    this.logger.Error(msg);
                }
                else
                {
                    var msg = "TypeException: {0}\r\nMessage: {1}\r\nStackTrace: {2}\r\n".F(ex.GetType().Name, ex.Message, ex.StackTrace);
                    this.logger.Error(msg);
                }
            }
        }

        private void ProcessWeatherData()
        {
            var exceptions = new ConcurrentQueue<Exception>();

            var links = this.linkRepository.Get().ToList();
            var weatherData = links.AsParallel().SelectMany(
                i =>
                {
                    try
                    {
                        return this.ProcessLink(i);
                    }
                    catch (Exception e)
                    {
                        exceptions.Enqueue(e);
                        return Enumerable.Empty<WeatherData>();
                    }
                }).ToList();

            this.weatherDataRepository.AddOrUpdate(weatherData);
            this.weatherDataRepository.Save();

            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }

        private IEnumerable<WeatherData> ProcessLink(Link link)
        {
            var result = Enumerable.Empty<WeatherData>();

            switch (link.TypeProvider)
            {
                case TypeProvider.Gismeteo:
                    result = this.gismeteoProvider.Fetch(link.Url);
                    break;

                case TypeProvider.Sinoptik:
                    result = this.sinoptikProvider.Fetch(link.Url);
                    break;

                case TypeProvider.Rp5:
                    result = this.rp5Provider.Fetch(link.Url);
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
