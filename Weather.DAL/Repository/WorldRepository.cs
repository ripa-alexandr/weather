
using System.Data.Entity;

using Weather.DAL.Repository.Abstract;
using Weather.Data.Entities;

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

