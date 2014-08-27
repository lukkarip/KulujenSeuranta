using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace KulujenSeuranta.Settings
{
    public class ConfigurationHandler
    {
        public string GetAppSettingValue(string key)
        {
            if (key == null)
            {
                return null;
            }

            string settingValue = ConfigurationManager.AppSettings[key];

            return settingValue;
        }
    }
}