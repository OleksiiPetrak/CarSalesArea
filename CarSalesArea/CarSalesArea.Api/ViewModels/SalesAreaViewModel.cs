using System.Collections.Generic;
using CarSalesArea.Core.Infrastructure;
using Newtonsoft.Json;

namespace CarSalesArea.Api.ViewModels
{
    /// <summary>
    /// Represents view model of sales area.
    /// </summary>
    public class SalesAreaViewModel: BaseViewModel, IEtaggable
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
        public IEnumerable<ManagerViewModel> Managers { get; set; }

        public string GetEtag()
        {
            var serialized = JsonConvert.SerializeObject(this);
            return Md5Hash.ForString(serialized);
        }
    }
}
