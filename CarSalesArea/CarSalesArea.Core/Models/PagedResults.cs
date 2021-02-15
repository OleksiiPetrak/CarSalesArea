using System.Collections.Generic;

namespace CarSalesArea.Core.Models
{
    public class PagedResults<T>   
    {
        public IEnumerable<T> Items { get; set; }
        public int TotalSize { get; set; }
    }
}
