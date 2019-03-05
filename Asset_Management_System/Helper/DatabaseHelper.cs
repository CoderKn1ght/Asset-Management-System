using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Asset_Management_System.Helper
{
    public class DatabaseHelper
    {
        public static string GetRDSConnectionString()
        {
            var appConfig = ConfigurationManager.AppSettings;

            var dbname = appConfig["assetmanagement"];

            if (string.IsNullOrEmpty(dbname)) return null;

            var username = appConfig["username"];
            var password = appConfig["password"];
            var hostname = appConfig["hostname"];
            var port = appConfig["1433"];

            return "Data Source=" + hostname + "," + port + ";Initial Catalog=" + dbname + ";User ID=" + username + ";Password=" + password + ";";


        }
    }
}
