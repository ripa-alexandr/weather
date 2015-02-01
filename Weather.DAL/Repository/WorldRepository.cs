
using System.Data.Entity;

using Weather.Common.Entities;
using Weather.DAL.Repository.Abstract;

namespace Weather.DAL.Repository
{
    public class WorldRepository : BaseRepositorty<World>
    {
        public WorldRepository(DbContext context)
            : base(context)
        {
        }
    }
}

