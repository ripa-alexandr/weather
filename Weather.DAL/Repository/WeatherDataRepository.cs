
using System.Data.Entity;

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
    }
}
