using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace SawDust
{
    internal class AppGlobals
    {
        public static string gAppName = "SawDust";

        // Documentation 
        // https://docs.microsoft.com/en-us/windows/uwp/design/app-settings/store-and-retrieve-app-data
        public static ApplicationDataContainer gLocalSettings = ApplicationData.Current.LocalSettings;
        public static StorageFolder gLocalFolder = ApplicationData.Current.LocalFolder;
        public static string gLogFilePath = null;

    }
}
