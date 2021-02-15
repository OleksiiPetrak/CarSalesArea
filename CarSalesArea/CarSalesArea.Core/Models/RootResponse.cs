using CarSalesArea.Core.Infrastructure;
using Newtonsoft.Json;

namespace CarSalesArea.Core.Models
{
    public class RootResponse: Resource, IEtaggable
    {
        public Link Managers { get; set; }
        public Link Cars { get; set; }
        public Link Areas { get; set; }
        public string GetEtag()
        {
            var serialized = JsonConvert.SerializeObject(this);
            return Md5Hash.ForString(serialized);
        }
    }
}
