
using System.Data.Entity;

using Weather.DAL.Repository.Abstract;
using Weather.Data.Entities;

namespace Weather.DAL.Repository
{
    public class RegionRepository : BaseRepositorty<Region>
    {
        public RegionRepository(DbContext context)
            : base(context)
        {
        }
    }
}
