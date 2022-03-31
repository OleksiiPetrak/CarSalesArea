using CarSalesArea.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarSalesArea.Data.Repositories.Interfaces
{
    /// <summary>
    /// Represents a data provider for managers.
    /// </summary>
    public interface IManagerRepository
    {
        /// <summary>
        /// Retrieves managerEntity by identifier.
        /// </summary>
        /// <param name="id">The managerEntity identifier.</param>
        /// <returns>ManagerEntity entity.</returns>
        Task<ManagerEntity> GetManagerByIdAsync(long id);

        /// <summary>
        /// Retrieves all existing managers.
        /// </summary>
        /// <returns>Managers collection.</returns>
        Task<IEnumerable<ManagerEntity>> GetAllManagersCollectionAsync();

        /// <summary>
        /// Creates new managerEntity.
        /// </summary>
        /// <param name="managerEntity">The managerEntity entity.</param>
        Task<long> CreateManagerAsync(ManagerEntity managerEntity);

        /// <summary>
        /// Update existing managerEntity.
        /// </summary>
        /// <param name="managerEntity">The managerEntity entity.</param>
        /// <returns></returns>
        Task UpdateManagerAsync(ManagerEntity managerEntity);

        /// <summary>
        /// Remove managerEntity by id.
        /// </summary>
        /// <param name="id">The managerEntity identifier.</param>
        /// <returns></returns>
        Task DeleteManagerAsync(long id);
    }
}
