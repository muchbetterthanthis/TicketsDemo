using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Mongo.Interfaces;

namespace TicketsDemo.Mongo.Implementations
{
    public class ConnectionStringProvider : IConnectionStringProvider
    {
        public string connectionString { get; set; } = ConfigurationManager.ConnectionStrings["MongoConnectionString"].ConnectionString;
    }
}
