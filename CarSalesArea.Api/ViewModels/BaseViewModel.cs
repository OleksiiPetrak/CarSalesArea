using CarSalesArea.Core.Models;
using Newtonsoft.Json;

namespace CarSalesArea.Api.ViewModels
{
    /// <summary>
    /// Represents base view model properties.
    /// </summary>
    public class BaseViewModel: Resource
    {
        /// <summary>
        /// The numeric id of entities.
        /// </summary>
        [JsonProperty(Order = -2)]
        public long Id { get; set; }
    }
}
