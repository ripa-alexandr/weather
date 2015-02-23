
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Weather.DAL.Repository.Interface
{
    public interface IRepository
    {
        /// <summary>
        /// Get all entities from database 
        /// </summary>
        /// <returns></returns>
        IQueryable<T> Get<T>() where T : class;

        /// <summary>
        /// Get entities from database with where 
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        IQueryable<T> Get<T>(Expression<Func<T, bool>> where) where T : class;

        /// <summary>
        /// Add entity to database
        /// </summary>
        /// <param name="data"></param>
        void Add<T>(T data) where T : class;

        /// <summary>
        /// Add or update entity to database
        /// </summary>
        /// <param name="source"></param>
        /// <param name="same"></param>
        /// <param name="where"></param>
        void AddOrUpdate<T>(IEnumerable<T> source, Func<T, T, bool> same, Expression<Func<T, bool>> where = null) where T : class;

        /// <summary>
        /// Delete entity from database
        /// </summary>
        /// <param name="data"></param>
        void Delete<T>(T data) where T : class;

        /// <summary>
        /// Save changes in database
        /// </summary>
        void Save();
    }
}
