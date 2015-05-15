using System;
using System.Configuration;

namespace Weather.Website
{
    public static class WebsiteConfig
    {
        public static int DefaultProvider
        {
            get
            {
                return Int32.Parse(ConfigurationManager.AppSettings["DefaultProvider"]);
            }
        }
    }
}