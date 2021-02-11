namespace CarSalesArea.Data.Models
{
    /// <summary>
    /// Represent entity of sales area's manager
    /// </summary>
    public class ManagerEntity: BaseEntity
    {
        /// <summary>
        /// The manager's name.
        /// </summary>
        public string ManagerName { get; set; }

        /// <summary>
        /// The manager's surname.
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// The manager's related sales area.
        /// </summary>
        public SalesArea SalesArea { get; set; }
    }
}
