
using System.Data.Entity;

using Weather.Common.Entities;
using Weather.DAL.Repository.Abstract;

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
