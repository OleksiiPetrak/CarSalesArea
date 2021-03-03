using CarSalesArea.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarSalesArea.Data.Repositories.Interfaces
{
    /// <summary>
    /// Represents a data provider for media files.
    /// </summary>
    public interface IMediaRepository
    {
        /// <summary>
        /// Retrieves photo by identifier.
        /// </summary>
        /// <param name="carId">The photo identifier.</param>
        /// <returns>Photo entity.</returns>
        Task<IEnumerable<PhotoEntity>> GetPhotoCollectionByCarIdAsync(long carId);

        /// <summary>
        /// Retrieves all existing photos.
        /// </summary>
        /// <returns>Photos collection.</returns>
        Task<IEnumerable<PhotoEntity>> GetAllPhotosCollectionAsync();

        /// <summary>
        /// Creates new photo.
        /// </summary>
        /// <param name="photoEntity">The photo entity.</param>
        Task<long> CreatePhotoAsync(PhotoEntity photoEntity);

        /// <summary>
        /// Update existing photo.
        /// </summary>
        /// <param name="photoEntity">The photo entity.</param>
        /// <returns></returns>
        Task UpdatePhotoAsync(PhotoEntity photoEntity);

        /// <summary>
        /// Remove photo by carId.
        /// </summary>
        /// <param name="id">The photo identifier.</param>
        /// <returns></returns>
        Task DeletePhotoAsync(long id);
    }
}
