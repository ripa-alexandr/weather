
namespace Weather.Common.Interfaces
{
    public interface IBaseEntity
    {
        /// <summary>
        /// Entity must have id
        /// </summary>
        int Id { get; set; }
    }
}
