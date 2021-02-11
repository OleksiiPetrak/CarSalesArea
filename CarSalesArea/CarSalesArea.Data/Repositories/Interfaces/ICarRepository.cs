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
        /// Retrieves car by identifier.
        /// </summary>
        /// <param name="id">The car identifier.</param>
        /// <returns>Car entity.</returns>
        Task<Car> GetCarByIdAsync(long id);

        /// <summary>
        /// Retrieves all existing cars.
        /// </summary>
        /// <returns>Cars collection.</returns>
        Task<IEnumerable<Car>> GetAllCarsCollectionAsync();

        /// <summary>
        /// Creates new car.
        /// </summary>
        /// <param name="manager">The car entity.</param>
        Task<long> CreateManagerAsync(Car car);

        /// <summary>
        /// Update existing car.
        /// </summary>
        /// <param name="manager">The car entity.</param>
        /// <returns></returns>
        Task UpdateCarAsync(Car car);

        /// <summary>
        /// Remove car by id.
        /// </summary>
        /// <param name="id">The car identifier.</param>
        /// <returns></returns>
        Task DeleteCarAsync(long id);
    }
}
