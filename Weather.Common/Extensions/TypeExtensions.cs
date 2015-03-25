using System;

namespace Weather.Common.Extensions
{
    public static class TypeExtensions
    {
        public static bool IsSqlCompatible(this Type type)
        {
            var t = Nullable.GetUnderlyingType(type) ?? type;

            return t.IsValueType || t == typeof(string);
        }
    }
}
