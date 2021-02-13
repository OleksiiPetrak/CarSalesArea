namespace CarSalesArea.Api.ViewModels
{
    public class PhotoViewModel
    {
        /// <summary>
        /// The photo url.
        /// </summary>
        public string PhotoPath { get; set; }

        /// <summary>
        /// The related car.
        /// </summary>
        public CarViewModel Car { get; set; }
    }
}
