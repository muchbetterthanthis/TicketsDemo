using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using TicketsDemo.Data.Entities;
using TicketsDemo.Mongo.Implementations;

namespace TicketsDemo.Mongo.Interfaces
{
    public interface ICollectionCreator
    {
        IMongoCollection<MongoTrain> Trains { get; }
    }
}
