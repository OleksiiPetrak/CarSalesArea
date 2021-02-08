using CarSalesArea.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarSalesArea.Data.Repositories.Interfaces
{
    /// <summary>
    /// Represents a data provider for sales area.
    /// </summary>
    public interface ISalesAreaRepository
    {
        /// <summary>
        /// Retrieves sales area by identifier.
        /// </summary>
        /// <param name="id">The manager identifier.</param>
        /// <returns>Sales area entity.</returns>
        Task<Manager> GetSalesAreaByIdAsync(long id);

        /// <summary>
        /// Retrieves all existing sales areas.
        /// </summary>
        /// <returns>sales area's collection.</returns>
        Task<IEnumerable<Manager>> GetAllSalesAreasCollectionAsync();

        /// <summary>
        /// Creates new sales area.
        /// </summary>
        /// <param name="manager">The sales area entity.</param>
        Task<long> CreateSalesAreaAsync(Manager manager);

        /// <summary>
        /// Update existing sales area.
        /// </summary>
        /// <param name="manager">The sales area entity.</param>
        /// <returns></returns>
        Task UpdateSalesAreaAsync(Manager manager);

        /// <summary>
        /// Remove sales area by id.
        /// </summary>
        /// <param name="id">The sales area identifier.</param>
        /// <returns></returns>
        Task DeleteSalesAreaAsync(long id);
    }
}
