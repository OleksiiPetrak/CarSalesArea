using System;

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

        public int AreaId { get; set; }

        public string FuelTypeId { get; set; }

        public string PhotoPath { get; set; }
    }
}
