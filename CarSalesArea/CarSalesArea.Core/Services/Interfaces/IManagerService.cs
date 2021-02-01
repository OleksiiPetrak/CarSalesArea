using CarSalesArea.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarSalesArea.Core.Services.Interfaces
{
    /// <summary>
    /// Represent manager service.
    /// </summary>
    public interface IManagerService
    {
        /// <summary>
        /// Retrieves manager by identifier.
        /// </summary>
        /// <param name="id">The manager's identifier./param>
        /// <returns>The manager entity.</returns>
        Task<Manager> GetManagerByIdAsync(long id);

        /// <summary>
        /// Gets all manager collection.
        /// </summary>
        /// <returns>The manager collection.</returns>
        Task<IEnumerable<Manager>> GetAllManagersAsync();

        /// <summary>
        /// Creates new manager record.
        /// </summary>
        /// <param name="manager"></param>
        Task CreateManagerAsync(Manager manager);

        /// <summary>
        /// Updates manager entity.
        /// </summary>
        /// <param name="manager"></param>
        Task UpdateManagerAsync(Manager manager);

        /// <summary>
        /// Removes manager entity by identifier.
        /// </summary>
        /// <param name="id">The manager identifier.</param>
        Task RemoveManagerAsync(long id);
    }
}
