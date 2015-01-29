
using System.Data.Entity;

using Weather.DAL.Repository.Abstract;
using Weather.Data.Entities;

namespace Weather.DAL.Repository
{
    public class CountryRepository : BaseRepositorty<Country>
    {
        public CountryRepository(DbContext context)
            : base(context)
        {
        }
    }
}
