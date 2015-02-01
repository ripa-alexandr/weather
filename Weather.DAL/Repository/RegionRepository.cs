
using System.Data.Entity;

using Weather.Common.Entities;
using Weather.DAL.Repository.Abstract;

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
