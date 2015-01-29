
namespace Weather.Data.Interfaces
{
    public interface IBaseEntity
    {
        /// <summary>
        /// Entity must have id
        /// </summary>
        int Id { get; set; }
    }
}
