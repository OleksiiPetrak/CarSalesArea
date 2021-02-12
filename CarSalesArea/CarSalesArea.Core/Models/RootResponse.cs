using System;
using System.Collections.Generic;
using System.Text;

namespace CarSalesArea.Core.Models
{
    public class RootResponse: Resource
    {
        public Link Managers { get; set; }
    }
}
