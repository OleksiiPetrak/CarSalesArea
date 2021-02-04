using System;
using System.Collections.Generic;
using System.Text;
using CarSalesArea.Core.Filters;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CarSalesArea.Core.Infrastructure
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class EtagAttribute: Attribute, IFilterFactory
    {
        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            return new EtagHeaderFilter();
        }

        public bool IsReusable => true;
    }
}
