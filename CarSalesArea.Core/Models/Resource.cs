using Newtonsoft.Json;

namespace CarSalesArea.Core.Models
{
    public abstract class Resource : Link
    {
        [JsonIgnore]
        public Link Self { get; set; }
    }
}
