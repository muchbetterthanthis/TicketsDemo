using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities;

namespace TicketsDemo.Mongo.Implementations
{

    public class MongoTrain
    {
        public ObjectId _id { get; set; }
        public int TrainId { get; set; }
        public int Number { get; set; }
        public string StartLocation { get; set; }
        public string EndLocation { get; set; }
        public List<Carriage> Carriages { get; set; }
    }
}
