using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Weather.WindowsServiceParser
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
#if DEBUG
            var wearherParser = new WeatherParser();
            wearherParser.Start();
#else
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
            { 
                new WeatherParser() 
            };
            ServiceBase.Run(ServicesToRun);
#endif
        }
    }
}
