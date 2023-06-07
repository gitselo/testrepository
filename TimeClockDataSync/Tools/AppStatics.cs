using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeClockDataSync.Tools
{
    public static class AppStatics
    {
        public static  string SambaSettingsFilePath = string.Format(@"{0}\{1}", 
            System.Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
            System.Configuration.ConfigurationManager.AppSettings["SambaSettingsFilePath"]
            );

        public static string SambaDbConnectionString = string.Format("{0}",
           System.Configuration.ConfigurationManager.AppSettings["SambaDbConnectionString"]
           );
    }
}
