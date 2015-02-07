using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace MiniFacts.BLL.Common
{
    public static class Constants
    {
        public static string ROOTURL = ConfigurationManager.AppSettings["UrlPath"].ToString();
    }
}