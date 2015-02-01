
using System.Data.Entity;

using Weather.Common.Entities;
using Weather.DAL.Repository.Abstract;

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
