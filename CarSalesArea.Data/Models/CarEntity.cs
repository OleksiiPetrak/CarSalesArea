using System;
using System.Collections.Generic;

namespace CarSalesArea.Data.Models
{
    /// <summary>
    /// Represent entity of car
    /// </summary>
    public class CarEntity: BaseEntity
    {
        /// <summary>
        /// The car's brand.
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// The car's model.
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// The car's year of manufacture.
        /// </summary>
        public DateTime Year { get; set; }

        /// <summary>
        /// The car's engine volume.
        /// </summary>
        public double EngineVolume { get; set; }

        /// <summary>
        /// The car's mileage.
        /// </summary>
        public int Mileage { get; set; }

        /// <summary>
        /// The car's description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The car's price.
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// The car's price.
        /// </summary>
        public string VinCode { get; set; }

        /// <summary>
        /// The car's body color.
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// The car's body type.
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// The car's related sales area.
        /// </summary>
        public SalesArea SalesArea { get; set; }

        /// <summary>
        /// The car's related fuel type.
        /// </summary>
        public FuelTypeEntity FuelType { get; set; }

        /// <summary>
        /// The car's related photos.
        /// </summary>
        public IEnumerable<PhotoEntity> Photos { get; set; }
    }
}
