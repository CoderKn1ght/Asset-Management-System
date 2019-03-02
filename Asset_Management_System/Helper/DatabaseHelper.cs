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

            var username = appConfig["shashank"];
            var password = appConfig["CSE5320#"];
            var hostname = appConfig["aa1tptq4804zunb.cm0hzguvk0l2.us-east-2.rds.amazonaws.com"];
            var port = appConfig["1433"];

            return "Data Source=" + hostname + "," + port + ";Initial Catalog=" + dbname + ";User ID=" + username + ";Password=" + password + ";";


        }
    }
}