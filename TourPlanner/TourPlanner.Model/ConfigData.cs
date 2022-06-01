using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.Model
{
    public class ConfigData
    {
        public string DALSqlAssembly { get; set; }
        public string DALApiAssembly { get; set; }
        public string ApiKey { get; set; }
        public string PostgresSqlConnectionString { get; set; }

        public ConfigData()
        {

        }
    }
}
