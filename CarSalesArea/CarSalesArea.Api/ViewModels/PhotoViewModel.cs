using CarSalesArea.Core.Infrastructure;
using Newtonsoft.Json;

namespace CarSalesArea.Api.ViewModels
{
    public class PhotoViewModel: BaseViewModel, IEtaggable
    {
        /// <summary>
        /// The photo url.
        /// </summary>
        public string PhotoPath { get; set; }

        /// <summary>
        /// The related car.
        /// </summary>
        public CarViewModel Car { get; set; }

        public string GetEtag()
        {
            var serialized = JsonConvert.SerializeObject(this);
            return Md5Hash.ForString(serialized);
        }
    }
}
