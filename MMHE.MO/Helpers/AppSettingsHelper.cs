using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMHE.MO.Helpers
{
    public class AppSettingsHelper
    {
        public static string ReportPath
        {
            get { return ConfigurationManager.AppSettings["ReportPath"]; }
        }
    }
}
