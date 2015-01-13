using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JXT.PrimaryKey.Batman.Web.ViewModel
{
    using JXT.PrimaryKey.Batman.Core.ManagerConfig;

    public class ViewMenuModel
    {
        public WebMenuElements MenuInformation { get; set; }

        public string AccessToken { get; set; }
    }
}