using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;

using AutoMapper;

using EntityFramework.BulkInsert.Extensions;

using Weather.Common.Entities;
using Weather.DAL.Repository.Interface;

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
            var destination = this.context.Set<WeatherDataEntity>().Where(x => x.DateTime >= time).ToList();
            
            var insert = source.Where(x => !destination.Any(y => same(x, y)));
            var update = source.Except(insert);

            //this.Context.BulkInsert(insert);
            //this.Context.BulkInsert(insert.Select(i => i.WeatherDescription));

            this.context.Database.SqlQuery<WeatherDataEntity>("dbo.WeatherData_Insert @Provider, @ProviderName, @DateTime, @CityId",
                new SqlParameter("Provider", 1),
                new SqlParameter("ProviderName", "CustomProvider"),
                new SqlParameter("DateTime", DateTime.Now),
                new SqlParameter("CityId", 1));

            foreach (var item in update)
            {
                Mapper.Map(item, destination.First(x => same(item, x)));
            }
        }
    }
}
