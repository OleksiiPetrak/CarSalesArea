using CarSalesArea.Core.Infrastructure;
using Newtonsoft.Json;

namespace CarSalesArea.Api.ViewModels
{
    /// <summary>
    /// Represent view model of sales area's manager
    /// </summary>
    public class ManagerViewModel: BaseViewModel, IEtaggable
    {
        /// <summary>
        /// The manager's name.
        /// </summary>
        public string ManagerName { get; set; }

        /// <summary>
        /// The manager's surname.
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// The manager's related sales area.
        /// </summary>
        public SalesAreaViewModel SalesArea { get; set; }

        public string GetEtag()
        {
            var serialized = JsonConvert.SerializeObject(this);
            return Md5Hash.ForString(serialized);
        }
    }
}
