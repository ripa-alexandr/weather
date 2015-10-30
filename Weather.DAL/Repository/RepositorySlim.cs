using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

using AutoMapper;

using EntityFramework.BulkInsert.Extensions;

using Weather.DAL.Entities;
using Weather.DAL.Repository.Interface;
using Weather.Utilities.Extensions;

namespace Weather.DAL.Repository
{
    public class RepositorySlim : IRepositorySlim
    {
        private readonly DbContext context;

        public RepositorySlim(DbContext context)
        {
            this.context = context;
        }

        public void AddOrUpdate(IEnumerable<WeatherDataEntity> source)
        {
            var time = source.Min(t => t.DateTime);
            var same = new Func<WeatherDataEntity, WeatherDataEntity, bool>((x, y) => x.DateTime == y.DateTime && x.Provider == y.Provider && x.CityId == y.CityId);
            var destination = this.context.Database.SqlQuery<WeatherDataEntity>("SELECT * FROM WeatherData WHERE DateTime >= {0}", time).ToList();

            var insert = source.Where(x => !destination.Any(y => same(x, y))).ToList();
            var update = source.Except(insert).ToList();

            this.context.BulkInsert(insert);
            
            foreach (var item in update)
            {
                var margeItem = Mapper.Map(item, destination.First(x => same(item, x)));
                
                this.context.Database.ExecuteSqlCommandSmart("WeatherData_Update", margeItem);
            }
        }
    }
}
