
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using Weather.Common.Interfaces;

namespace Weather.DAL.Repository.Interface
{
    public interface IRepository
    {
        /// <summary>
        /// Get all entities from database 
        /// </summary>
        /// <returns></returns>
        IQueryable<T> Get<T>() where T : class, IBaseEntity;

        /// <summary>
        /// Get entities from database with where 
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        IQueryable<T> Get<T>(Expression<Func<T, bool>> where) where T : class, IBaseEntity;

        /// <summary>
        /// Add entity to database
        /// </summary>
        /// <param name="data"></param>
        void Add<T>(T data) where T : class, IBaseEntity;

        /// <summary>
        /// Add or update entities to database for all entities in database
        /// </summary>
        /// <param name="source"></param>
        /// <param name="same"></param>
        void AddOrUpdate<T>(IEnumerable<T> source, Func<T, T, bool> same) where T : class, IBaseEntity;

        /// <summary>
        /// Add or update entities to database for some entities in database
        /// </summary>
        /// <param name="source"></param>
        /// <param name="same"></param>
        /// <param name="where"></param>
        void AddOrUpdate<T>(IEnumerable<T> source, Func<T, T, bool> same, Expression<Func<T, bool>> where) where T : class, IBaseEntity;

        /// <summary>
        /// Delete entity from database
        /// </summary>
        /// <param name="data"></param>
        void Delete<T>(T data) where T : class, IBaseEntity;

        /// <summary>
        /// Save changes in database
        /// </summary>
        void Save();
    }
}
