
using System;

namespace Weather.Utilities
{
    public static class UrlUtility
    {
        public static string Combine(string baseUri, string relativeUri)
        {
            return new Uri(new Uri(baseUri), relativeUri).ToString();
        }
    }
}
