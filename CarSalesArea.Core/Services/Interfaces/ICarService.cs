using CarSalesArea.Core.Models;
using System.Threading.Tasks;

namespace CarSalesArea.Core.Services.Interfaces
{
    /// <summary>
    /// Represent car service.
    /// </summary>
    public interface ICarService
    {
        /// <summary>
        /// Retrieves car by identifier.
        /// </summary>
        /// <param name="id">The car's identifier./param>
        /// <returns>The car entity.</returns>
        Task<CarModel> GetCarByIdAsync(long id);

        /// <summary>
        /// Gets all car collection.
        /// </summary>
        /// <returns>The car collection.</returns>
        Task<PagedResults<CarModel>> GetAllCarsAsync(PagingOptions pagingOptions);

        /// <summary>
        /// Creates new car record.
        /// </summary>
        /// <param name="car"></param>
        Task<long> CreateCarAsync(CarModel car);

        /// <summary>
        /// Updates car entity.
        /// </summary>
        /// <param name="car"></param>
        Task UpdateCarAsync(CarModel car);

        /// <summary>
        /// Removes car entity by identifier.
        /// </summary>
        /// <param name="id">The car identifier.</param>
        Task RemoveCarAsync(long id);
    }
}
