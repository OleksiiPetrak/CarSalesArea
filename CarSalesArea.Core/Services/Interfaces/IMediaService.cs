using CarSalesArea.Core.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarSalesArea.Core.Services.Interfaces
{
    public interface IMediaService
    {
        /// <summary>
        /// Retrieves photo by car identifier.
        /// </summary>
        /// <param name="carId">The car identifier.</param>
        /// <returns>Photo models.</returns>
        Task<IEnumerable<PhotoModel>> GetPhotoCollectionByCarIdAsync(long carId);

        /// <summary>
        /// Retrieves all existing photos.
        /// </summary>
        /// <returns>Photos collection.</returns>
        Task<IEnumerable<PhotoModel>> GetAllPhotosCollectionAsync();

        /// <summary>
        /// Creates new photo.
        /// </summary>
        /// <param name="photoModel">The photo model.</param>
        Task<long> CreatePhotoAsync(PhotoModel photoModel);

        /// <summary>
        /// Add media files for car.
        /// </summary>
        /// <param name="carId">Id of selected car.</param>
        /// <param name="mediaFiles">Collection of media files.</param>
        /// <returns></returns>
        Task<IEnumerable<long>> CreateCarMediaAsync(long carId, IEnumerable<IFormFile> mediaFiles);

        /// <summary>
        /// Update existing photo.
        /// </summary>
        /// <param name="photoModel">The photo model.</param>
        /// <returns></returns>
        Task UpdatePhotoAsync(PhotoModel photoModel);

        /// <summary>
        /// Remove photo by carId.
        /// </summary>
        /// <param name="id">The photo identifier.</param>
        /// <returns></returns>
        Task DeletePhotoAsync(long id);
    }
}
