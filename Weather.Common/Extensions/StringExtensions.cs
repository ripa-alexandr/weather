
namespace Weather.Common.Extensions
{
    public static class StringExtensions
    {
        public static string F(this string value, params object[] parameters)
        {
            return string.Format(value, parameters);
        }
    }
}
