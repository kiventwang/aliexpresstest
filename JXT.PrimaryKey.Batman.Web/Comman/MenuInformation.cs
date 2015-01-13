using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JXT.PrimaryKey.Batman.Core.ManagerConfig;

namespace JXT.PrimaryKey.Batman.Web.Comman
{
    using JXT.PrimaryKey.Batman.Core.ManagerConfig;

    public class MenuInformation
    {
        public static WebMenuElements GetWebMenuElement()
        {
            var configSection = ConfigManager.GetSection<WebMenuSection>("menuSection");
            if (configSection != null) return configSection.MenuInformation;
            return null;
        }
    }
}