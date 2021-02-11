using System;
using System.Collections.Generic;
using CarSalesArea.Core.Infrastructure;
using Newtonsoft.Json;

namespace CarSalesArea.Core.Models
{
    public class CarModel: BaseModel, IEtaggable
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
        public SalesAreaModel SalesArea { get; set; }

        /// <summary>
        /// The car's related fuel type.
        /// </summary>
        public FuelTypeModel FuelType { get; set; }

        /// <summary>
        /// The car's related photos.
        /// </summary>
        public IEnumerable<PhotoModel> Photos { get; set; }

        public string GetEtag()
        {
            var serialized = JsonConvert.SerializeObject(this);
            return Md5Hash.ForString(serialized);
        }
    }
}
