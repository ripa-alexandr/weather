using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Web;

namespace Weather.Website.Helpers
{
    public static class CultureHelpers
    {
        public static CultureInfo GetImplementedCulture(ResourceManager rm, string cultureName)
        {
            var cultures = GetImplementedCultures(rm);

            return cultures.FirstOrDefault(i => i.Name.Equals(cultureName, StringComparison.InvariantCultureIgnoreCase) || i.Name == string.Empty);
        }

        public static IEnumerable<CultureInfo> GetImplementedCultures(ResourceManager rm)
        {
            var cultures = CultureInfo.GetCultures(CultureTypes.AllCultures)
                .Where(i => rm.GetResourceSet(i, true, false) != null);

            return cultures;
        }
    }
}