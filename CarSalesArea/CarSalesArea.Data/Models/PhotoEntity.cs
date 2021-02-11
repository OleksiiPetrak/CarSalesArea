namespace CarSalesArea.Data.Models
{
    /// <summary>
    /// Represents photo entity.
    /// </summary>
    public class PhotoEntity
    {
        /// <summary>
        /// The photo url.
        /// </summary>
        public string PhotoPath { get; set; }

        /// <summary>
        /// The related car.
        /// </summary>
        public CarEntity Car { get; set; }
    }
}
