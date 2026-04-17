using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginWithOTP.DbLayer.Models
{
    public class MongoDbSettings
    {
        public required string ConnectionString { get; set; }
        public required string DatabaseName { get; set; }
        public required Dictionary<string, string> Collections { get; set; }
    }
}
