
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

using AutoMapper;

using Weather.DAL.Repository.Abstract;
using Weather.Data.Entities;

namespace Weather.DAL.Repository
{
    public class CityRepository : BaseRepositorty<City>
    {
        public CityRepository(DbContext context)
            : base(context)
        {
        }

        public void AddOrUpdateWeatherData(City city, ICollection<WeatherData> source)
        {
            foreach (var item in source)
            {
                item.City = city;
            }

            this.AddOrUpdate(source, city.WeatherData, (x, y) => x.DateTime == y.DateTime && x.TypeProvider == y.TypeProvider);
        }

        private void AddOrUpdate<T>(IEnumerable<T> source, ICollection<T> destination, Func<T, T, bool> same)
        {
            var insert = source.Where(x => !destination.Any(y => same(x, y))).ToArray();
            var update = source.Except(insert).ToArray();

            foreach (var item in insert)
            {
                destination.Add(item);
            }

            foreach (var item in update)
            {
                Mapper.Map(item, destination.First(x => same(item, x)));
            }
        }
    }
}
