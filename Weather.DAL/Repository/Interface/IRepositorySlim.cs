using System.Collections.Generic;

using Weather.Common.Entities;

namespace Weather.DAL.Repository.Interface
{
    public interface IRepositorySlim
    {
        /// <summary>
        /// Add or update WeatherData to database
        /// </summary>
        /// <param name="source"></param>
        void AddOrUpdate(IEnumerable<WeatherDataEntity> source);
    }
}
