using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CareerCloud.EntityFrameworkDataAccess
{
    class BaseConnection
    {
        public string connString;
        public BaseConnection()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            connString = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
        }
    }
}
