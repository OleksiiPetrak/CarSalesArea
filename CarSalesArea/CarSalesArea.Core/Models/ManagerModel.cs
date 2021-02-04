using CarSalesArea.Core.Infrastructure;
using Newtonsoft.Json;

namespace CarSalesArea.Core.Models
{
    /// <summary>
    /// Represent model of sales area's manager
    /// </summary>
    public class ManagerModel: BaseModel, IEtaggable
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
        public SalesAreaModel SalesArea { get; set; }

        public string GetEtag()
        {
            var serialized = JsonConvert.SerializeObject(this);
            return Md5Hash.ForString(serialized);
        }
    }
}
