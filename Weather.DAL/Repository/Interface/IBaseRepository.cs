
using System.Linq;

namespace Weather.DAL.Repository.Interface
{
    public interface IBaseRepository<T>
    {
        /// <summary>
        /// Get one entity from database with id 
        /// </summary>
        /// <param name="id"></param>
        ///<returns></returns>
        T Get(int id);

        /// <summary>
        /// Get all entities from database
        /// </summary>
        ///<returns>return IQueryable</returns>
        IQueryable<T> Get();

        /// <summary>
        /// Get some entities from database
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        ///<returns>return IQueryable</returns>
        IQueryable<T> Get(int skip, int take);

        /// <summary>
        /// Add entity to database
        /// </summary>
        /// <param name="data"></param>
        void Add(T data);

        /// <summary>
        /// Delete entity from database
        /// </summary>
        /// <param name="data"></param>
        void Delete(T data);

        /// <summary>
        /// Save changes in database
        /// </summary>
        void Save();
    }
}
