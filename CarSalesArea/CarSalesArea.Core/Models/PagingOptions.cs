using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.Data.SqlClient;

namespace CarSalesArea.Core.Models
{
    /// <summary>
    /// Represent options to retrieve <see cref="PagingOptions"/>
    /// </summary>
    public class PagingOptions
    {
        /// <summary>
        /// Size of skipped items.
        /// </summary>
        [Range(0, 999999, ErrorMessage = "Offset must be not negative number")]
        public int? Offset { get; set; } = 0;

        /// <summary>
        /// Size of selected items.
        /// </summary>
        [Range(1, 100, ErrorMessage = "Limit must be greater than 0 and less than 100")]
        public int? Limit { get; set; } = 25;

        /// <summary>
        /// List of searching across all items.
        /// </summary>
        public Dictionary<string, string> Search { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// List of properties orders.
        /// </summary>
        public Dictionary<string, SortOrder> Sort { get; set; } = new Dictionary<string, SortOrder>();
    }
}
