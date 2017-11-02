using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternsForPagesTask.WebDriver
{
    class Configuration
    {
        public static string GetEnviromentVar(string var, string defaultValue)
        {
            return ConfigurationManager.AppSettings[var] ?? defaultValue;
        }

        public static string ElementTimeout => GetEnviromentVar("ElementTimeout", "30");
        public static string Browser => GetEnviromentVar("Browser", "FireFox");        
    }
}
