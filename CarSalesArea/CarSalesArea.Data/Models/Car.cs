﻿using System;

namespace CarSalesArea.Data.Models
{
    public class Car: BaseEntity
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
        public SalesArea SalesArea { get; set; }

        /// <summary>
        /// The car's related fuel type.
        /// </summary>
        public FuelTypeEntity FuelType { get; set; }

        /// <summary>
        /// The car's related photo.
        /// </summary>
        public PhotoEntity PhotoEntityPath { get; set; }
    }
}
