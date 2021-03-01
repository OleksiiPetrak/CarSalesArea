using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace CarSalesArea.Data.Models
{
    public class PagingOptions
    {
        /// <summary>
        /// Size of skipped items.
        /// </summary>
        public int Offset { get; set; } = 0;

        /// <summary>
        /// Size of selected items.
        /// </summary>
        public int Limit { get; set; } = 25;

        /// <summary>
        /// List of searching across all items.
        /// </summary>
        public Dictionary<int, string> Search { get; set; } = new Dictionary<int, string>();

        /// <summary>
        /// List of properties orders.
        /// </summary>
        public Dictionary<string, SortOrder> Sort { get; set; } = new Dictionary<string, SortOrder>();
    }
}
