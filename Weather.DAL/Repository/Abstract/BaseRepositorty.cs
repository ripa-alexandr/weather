
using System.Data.Entity;
using System.Linq;

using Weather.DAL.Repository.Interface;
using Weather.Data.Interfaces;

namespace Weather.DAL.Repository.Abstract
{
    public abstract class BaseRepositorty<T> : IBaseRepository<T> where T : class, IBaseEntity, new()
    {
        private readonly DbContext context;

        protected BaseRepositorty(DbContext context)
        {
            this.context = context;
        }

        public T Get(int id)
        {
            return this.context.Set<T>().FirstOrDefault(o => o.Id == id);
        }

        public IQueryable<T> Get()
        {
            return this.context.Set<T>().Select(o => o);
        }

        public IQueryable<T> Get(int skip, int take)
        {
            return this.Get().OrderBy(i => i.Id).Skip(skip).Take(take);
        }

        public void Add(T data)
        {
            this.context.Set<T>().Add(data);
        }

        public void Delete(T data)
        {
            this.context.Set<T>().Remove(data);
        }

        public void Save()
        {
            this.context.SaveChanges();
        }
    }
}
