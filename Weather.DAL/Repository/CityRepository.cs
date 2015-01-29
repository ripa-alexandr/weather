
using System.Data.Entity;

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
    }
}
