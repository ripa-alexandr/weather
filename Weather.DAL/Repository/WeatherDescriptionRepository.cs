
using System.Data.Entity;

using Weather.Common.Entities;
using Weather.DAL.Repository.Abstract;

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
