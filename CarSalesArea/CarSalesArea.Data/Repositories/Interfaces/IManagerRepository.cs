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
        /// Retrieves manager by identifier.
        /// </summary>
        /// <param name="id">The manager identifier.</param>
        /// <returns>Manager entity.</returns>
        Task<Manager> GetManagerByIdAsync(long id);

        /// <summary>
        /// Retrieves all existing managers.
        /// </summary>
        /// <returns>Managers collection.</returns>
        Task<IEnumerable<Manager>> GetAllManagersCollectionAsync();

        /// <summary>
        /// Creates new manager.
        /// </summary>
        /// <param name="manager">The manager entity.</param>
        Task<long> CreateManagerAsync(Manager manager);

        /// <summary>
        /// Update existing manager.
        /// </summary>
        /// <param name="manager">The manager entity.</param>
        /// <returns></returns>
        Task UpdateManagerAsync(Manager manager);

        /// <summary>
        /// Remove manager by id.
        /// </summary>
        /// <param name="id">The manager identifier.</param>
        /// <returns></returns>
        Task DeleteManagerAsync(long id);
    }
}
