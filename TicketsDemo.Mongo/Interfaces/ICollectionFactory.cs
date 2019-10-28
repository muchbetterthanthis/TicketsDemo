using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsDemo.Mongo.Interfaces
{
    public interface ICollectionFactory
    {
        ICollectionCreator CreateCollection();
        IMongoCollection<T> CreateCollection<T>(string collectionName);
    }
}
