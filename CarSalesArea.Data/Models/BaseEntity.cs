using Newtonsoft.Json;

namespace CarSalesArea.Data.Models
{
    /// <summary>
    /// Represents base entity properties.
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// The numeric id of entities.
        /// </summary>
        [JsonProperty(Order = -2)]
        public long Id { get; set; }
    }
}
