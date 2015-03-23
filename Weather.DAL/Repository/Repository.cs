
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

using AutoMapper;

using Weather.Common.Interfaces;
using Weather.DAL.Repository.Interface;

namespace Weather.DAL.Repository
{
    public class Repository : IRepository
    {
        private readonly DbContext context;

        public Repository(DbContext context)
        {
            this.context = context;
        }

        public IQueryable<T> Get<T>() where T : class, IBaseEntity
        {
            return this.Get<T>(i => true);
        }

        public IQueryable<T> Get<T>(Expression<Func<T, bool>> where) where T : class, IBaseEntity
        {
            return this.context.Set<T>().Where(where);
        }

        public void Add<T>(T data) where T : class, IBaseEntity
        {
            this.context.Set<T>().Add(data);
        }

        public void AddOrUpdate<T>(IEnumerable<T> source, Func<T, T, bool> same) where T : class, IBaseEntity
        {
            this.AddOrUpdate(source, same, i => true);
        }

        public void AddOrUpdate<T>(IEnumerable<T> source, Func<T, T, bool> same, Expression<Func<T, bool>> where) where T : class, IBaseEntity
        {
            var destination = this.Get(where).ToList();
            var insert = source.Where(x => !destination.Any(y => same(x, y)));
            var update = source.Except(insert);

            this.context.Set<T>().AddRange(insert);
            
            foreach (var item in update)
            {
                Mapper.Map(item, destination.First(x => same(item, x)));
            }
        }

        public void Delete<T>(T data) where T : class, IBaseEntity
        {
            this.context.Set<T>().Remove(data);
        }

        public void Save()
        {
            this.context.SaveChanges();
        }
    }
}
