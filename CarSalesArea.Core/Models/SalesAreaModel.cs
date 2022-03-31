using System.Collections.Generic;
using CarSalesArea.Core.Infrastructure;
using Newtonsoft.Json;

namespace CarSalesArea.Core.Models
{
    /// <summary>
    /// Represents model of sales area.
    /// </summary>
    public class SalesAreaModel: BaseModel
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
        public IEnumerable<ManagerModel> Managers { get; set; }
    }
}
