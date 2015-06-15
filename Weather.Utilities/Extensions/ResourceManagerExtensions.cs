
using System.Globalization;
using System.Resources;

namespace Weather.Utilities.Extensions
{
    public static class ResourceManagerExtensions
    {
        public static string GetLocalizeString(this ResourceManager rm, string name, CultureInfo culture)
        {
            var defaultCulture = CultureInfo.GetCultureInfo("");

            if (defaultCulture.Equals(culture))
                return rm.GetString(name, defaultCulture);

            var defaultVal = rm.GetString(name, defaultCulture);
            var val = rm.GetString(name, culture);

            return defaultVal != val ? val : null;
        }
    }
}
