using System;
using System.Collections.Generic;
using System.Linq;

using Weather.Common.Enums;

namespace Weather.Website
{
    public static class WebsiteConfig
    {
        public static IEnumerable<Provider> DefaultProviders
        {
            get
            {
                return Enum.GetValues(typeof(Provider)).Cast<Provider>();
            }
        }
    }
}