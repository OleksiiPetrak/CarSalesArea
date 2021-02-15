using System;
using System.ComponentModel.DataAnnotations;

namespace CarSalesArea.Core.Models
{
    public class PagingOptions
    {
        [Range(0, 999999, ErrorMessage = "Offset must be not negative number")]
        public int? Offset { get; set; } = 0;

        [Range(1, 100, ErrorMessage = "Limit must be greater than 0 and less than 100")]

        public int? Limit { get; set; } = 25;
    }
}
