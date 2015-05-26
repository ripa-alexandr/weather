using System;
using System.Collections.Generic;
using System.Linq;

using Weather.Common.Enums;

namespace Weather.Website
{
    public static class WebsiteConfig
    {
        public static IEnumerable<ProviderType> DefaultProviders
        {
            get
            {
                return Enum.GetValues(typeof(ProviderType)).Cast<ProviderType>();
            }
        }
    }
}