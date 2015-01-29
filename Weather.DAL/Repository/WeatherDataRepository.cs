
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

using Weather.DAL.Repository.Abstract;
using Weather.Data.Entities;

namespace Weather.DAL.Repository
{
    public class WeatherDataRepository : BaseRepositorty<WeatherData>
    {
        public WeatherDataRepository(DbContext context)
            : base(context)
        {
        }

        public void AddOrUpdate(IEnumerable<WeatherData> source)
        {
            var time = source.Min(t => t.DateTime);
            var destination = this.Get().Where(i => i.DateTime >= time).ToList();

            this.AddOrUpdate(source, destination, (x, y) => x.DateTime == y.DateTime && x.TypeProvider == y.TypeProvider && x.CityId == y.CityId);
        }
    }
}
