using CarSalesArea.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarSalesArea.Core.Models;

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
        Task<ManagerModel> GetManagerByIdAsync(long id);

        /// <summary>
        /// Gets all manager collection.
        /// </summary>
        /// <returns>The manager collection.</returns>
        Task<IEnumerable<ManagerModel>> GetAllManagersAsync();

        /// <summary>
        /// Creates new manager record.
        /// </summary>
        /// <param name="manager"></param>
        Task<long> CreateManagerAsync(ManagerModel manager);

        /// <summary>
        /// Updates manager entity.
        /// </summary>
        /// <param name="manager"></param>
        Task UpdateManagerAsync(ManagerModel manager);

        /// <summary>
        /// Removes manager entity by identifier.
        /// </summary>
        /// <param name="id">The manager identifier.</param>
        Task RemoveManagerAsync(long id);
    }
}
