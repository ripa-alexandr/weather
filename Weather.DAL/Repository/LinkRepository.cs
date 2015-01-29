
using System.Data.Entity;

using Weather.DAL.Repository.Abstract;
using Weather.Data.Entities;

namespace Weather.DAL.Repository
{
    public class LinkRepository : BaseRepositorty<Link>
    {
        public LinkRepository(DbContext context)
            : base(context)
        {
        }
    }
}
