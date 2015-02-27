
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using AutoMapper;

using Ninject;

using NLog;

using Weather.Bootstrap;
using Weather.Common.Dto;
using Weather.Common.Entities;
using Weather.Common.Enums;
using Weather.Common.Exceptions;
using Weather.Common.Extensions;
using Weather.DAL.Repository;
using Weather.DAL.Repository.Interface;
using Weather.Parser;

namespace Weather.WindowsServiceParser
{
    public class Processor
    {
        private readonly IRepository repository;
        private readonly SinoptikProvider sinoptikProvider;
        private readonly GismeteoProvider gismeteoProvider;
        private readonly Rp5Provider rp5Provider;
        private readonly Logger logger;

        public Processor(Logger logger)
        {
            var kernel = Kernel.Initialize();

            this.repository = kernel.Get<Repository>();
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

            var links = this.repository.Get<LinkEntity>().ToList();
            var weatherData = links.AsParallel().SelectMany(
                i =>
                {
                    try
                    {
                        return this.ProcessLink(Mapper.Map<LinkDto>(i));
                    }
                    catch (Exception e)
                    {
                        exceptions.Enqueue(e);
                        return Enumerable.Empty<WeatherDataDto>();
                    }
                }).ToList();

            this.SaveWeatherData(weatherData);

            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }

        private IEnumerable<WeatherDataDto> ProcessLink(LinkDto link)
        {
            var result = Enumerable.Empty<WeatherDataDto>();

            switch (link.Provider)
            {
                case ProviderType.Gismeteo:
                    result = this.gismeteoProvider.Fetch(link.Url);
                    break;

                case ProviderType.Sinoptik:
                    result = this.sinoptikProvider.Fetch(link.Url);
                    break;

                case ProviderType.Rp5:
                    result = this.rp5Provider.Fetch(link.Url);
                    break;
            }

            foreach (var item in result)
            {
                item.CityId = link.CityId;
            }

            return result;
        }

        private void SaveWeatherData(IEnumerable<WeatherDataDto> weatherData)
        {
            var time = weatherData.Min(t => t.DateTime);
            this.repository.AddOrUpdate(
                Mapper.Map<IEnumerable<WeatherDataEntity>>(weatherData),
                (x, y) => x.DateTime == y.DateTime && x.Provider == y.Provider && x.CityId == y.CityId,
                x => x.DateTime >= time);
            this.repository.Save();
        }
    }
}
