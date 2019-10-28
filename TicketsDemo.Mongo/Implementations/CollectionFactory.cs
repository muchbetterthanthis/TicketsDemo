using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Mongo.Interfaces;

namespace TicketsDemo.Mongo.Implementations
{
    public class CollectionFactory : ICollectionFactory
    {
        public IConnectionStringProvider _csp { get; set; }

        public CollectionFactory(IConnectionStringProvider csp)
        {
            _csp = csp;
        }

        public ICollectionCreator CreateCollection()
        {
            return new CollectionCreator(_csp);
        }

        public IMongoCollection<T> CreateCollection<T>(string collectionName)
        {
            MongoClient _client = new MongoClient(_csp.connectionString);
            IMongoDatabase MDB = _client.GetDatabase("MongoTrainRepo");

            return MDB.GetCollection<T>(collectionName);
        }
    }
}
