using CarSalesArea.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarSalesArea.Core.Services.Interfaces
{
    /// <summary>
    /// Represent sales area service.
    /// </summary>
    public interface ISalesAreaService
    {
        /// <summary>
        /// Retrieves sales area by identifier.
        /// </summary>
        /// <param name="id">The sales area's identifier./param>
        /// <returns>The sales area entity.</returns>
        Task<SalesAreaModel> GetSalesAreaByIdAsync(long id);

        /// <summary>
        /// Gets all sales areas collection.
        /// </summary>
        /// <returns>The sales area collection.</returns>
        Task<IEnumerable<SalesAreaModel>> GetAllSalesAreasAsync();

        /// <summary>
        /// Creates new sales area record.
        /// </summary>
        /// <param name="salesAreaModel"></param>
        Task<long> CreateSalesAreaAsync(SalesAreaModel salesAreaModel);

        /// <summary>
        /// Updates sales area entity.
        /// </summary>
        /// <param name="salesAreaModel"></param>
        Task UpdateSalesAreaAsync(SalesAreaModel salesAreaModel);

        /// <summary>
        /// Removes sales area entity by identifier.
        /// </summary>
        /// <param name="id">The sales area identifier.</param>
        Task RemoveSalesAreaAsync(long id);
    }
}
