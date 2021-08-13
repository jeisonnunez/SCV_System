﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class AppSetting
    {
        Configuration config;

        public AppSetting()
        {
            config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

        }

        public string GetConnectionString(string key)
        {
            string cadena = config.ConnectionStrings.ConnectionStrings[key].ConnectionString;
            
            //config.Save(ConfigurationSaveMode.Modified);

            return cadena;
        }

        public void SaveConnectionString(string key, string value)
        {

            
            config.ConnectionStrings.ConnectionStrings[key].ConnectionString = value;
         
            config.ConnectionStrings.ConnectionStrings[key].ProviderName = "System.Data.SqlClient";

            config.Save(ConfigurationSaveMode.Modified);
        }

    }
}
