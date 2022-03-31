using Newtonsoft.Json;

namespace CarSalesArea.Core.Models
{
    /// <summary>
    /// Represents base model properties.
    /// </summary>
    public class BaseModel: Resource
    {
        /// <summary>
        /// The numeric id of entities.
        /// </summary>
        [JsonProperty(Order = -2)]
        public long Id { get; set; }
    }
}
