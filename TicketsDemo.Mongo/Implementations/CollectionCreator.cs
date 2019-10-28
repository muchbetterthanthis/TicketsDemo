using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities;
using TicketsDemo.Mongo.Interfaces;

namespace TicketsDemo.Mongo.Implementations
{
    public class CollectionCreator : ICollectionCreator
    {
        public MongoClient _client { get; set; }
        public IMongoDatabase MDB { get; set; }

        public IMongoCollection<MongoTrain> Trains
        {
            get { return MDB.GetCollection<MongoTrain>("MongoTrain"); }
        }

        public CollectionCreator (IConnectionStringProvider cs)
        {
            _client = new MongoClient(cs.connectionString);
            MDB = _client.GetDatabase("MongoTrainRepo");
        }
    }
}
