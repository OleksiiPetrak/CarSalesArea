namespace CarSalesArea.Core.Models
{
    /// <summary>
    /// Represents photo model.
    /// </summary>
    public class PhotoModel
    {
        /// <summary>
        /// The photo url.
        /// </summary>
        public string PhotoPath { get; set; }

        /// <summary>
        /// The related car.
        /// </summary>
        public CarModel Car { get; set; }
    }
}
