using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace ASPNETMVC5WebApp.Config
{
    public class Config_Connection
    {
        public static string GetConnection()
        {
            return ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;
        }

        //
    }
}