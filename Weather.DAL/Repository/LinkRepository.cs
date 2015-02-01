
using System.Data.Entity;

using Weather.Common.Entities;
using Weather.DAL.Repository.Abstract;

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
