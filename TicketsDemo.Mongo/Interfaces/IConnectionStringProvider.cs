using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsDemo.Mongo.Interfaces
{
    public interface IConnectionStringProvider
    {
        string connectionString { get; set; }
    }
}
