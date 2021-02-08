using System.Collections.Generic;
using System.Linq;

namespace CarSalesArea.Data.Models
{
    /// <summary>
    /// Represents entity of sales area.
    /// </summary>
    public class SalesArea: BaseEntity
    {
        /// <summary>
        /// The sales area allocation.
        /// </summary>
        public string AreaLocation { get; set; }

        /// <summary>
        /// The car capacity of sales area.
        /// </summary>
        public int Capacity { get; set; }

        /// <summary>
        /// The collection of sales area managers.
        /// </summary>
        public IEnumerable<Manager> Managers { get; set; }
    }
}
