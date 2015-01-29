
using System.Data.Entity;

using Weather.DAL.Repository.Abstract;
using Weather.Data.Entities;

namespace Weather.DAL.Repository
{
    public class WeatherDescriptionRepository : BaseRepositorty<WeatherDescription>
    {
        public WeatherDescriptionRepository(DbContext context)
            : base(context)
        {
        }
    }
}
