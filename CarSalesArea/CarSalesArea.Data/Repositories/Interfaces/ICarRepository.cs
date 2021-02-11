using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CarSalesArea.Data.Models;

namespace CarSalesArea.Data.Repositories.Interfaces
{
    /// <summary>
    /// Represents a data provider for cars.
    /// </summary>
    public interface ICarRepository
    {
        /// <summary>
        /// Retrieves carEntity by identifier.
        /// </summary>
        /// <param name="id">The carEntity identifier.</param>
        /// <returns>CarEntity entity.</returns>
        Task<CarEntity> GetCarByIdAsync(long id);

        /// <summary>
        /// Retrieves all existing cars.
        /// </summary>
        /// <returns>Cars collection.</returns>
        Task<IEnumerable<CarEntity>> GetAllCarsCollectionAsync();

        /// <summary>
        /// Creates new carEntity.
        /// </summary>
        /// <param name="manager">The carEntity entity.</param>
        Task<long> CreateManagerAsync(CarEntity carEntity);

        /// <summary>
        /// Update existing carEntity.
        /// </summary>
        /// <param name="manager">The carEntity entity.</param>
        /// <returns></returns>
        Task UpdateCarAsync(CarEntity carEntity);

        /// <summary>
        /// Remove carEntity by id.
        /// </summary>
        /// <param name="id">The carEntity identifier.</param>
        /// <returns></returns>
        Task DeleteCarAsync(long id);
    }
}
