using CarSalesArea.Core.Infrastructure;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CarSalesArea.Api.ViewModels
{
    public class CarViewModel: BaseViewModel, IEtaggable
    {
        public string Brand { get; set; }

        public string Model { get; set; }

        public DateTime Year { get; set; }

        public double EngineVolume { get; set; }

        public int Mileage { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public string VinCode { get; set; }

        public string Color { get; set; }

        public string Body { get; set; }

        /// <summary>
        /// The car's related sales area.
        /// </summary>
        public SalesAreaViewModel SalesArea { get; set; }

        /// <summary>
        /// The car's related fuel type.
        /// </summary>
        public FuelTypeViewModel FuelType { get; set; }

        /// <summary>
        /// The car's related photos.
        /// </summary>
        public IEnumerable<PhotoViewModel> Photos { get; set; }

        /// <summary>
        /// The car's media file collection for storage.
        /// </summary>
        public IEnumerable<IFormFile> Files { get; set; }

        public string GetEtag()
        {
            var serialized = JsonConvert.SerializeObject(this);
            return Md5Hash.ForString(serialized);
        }
    }
}
