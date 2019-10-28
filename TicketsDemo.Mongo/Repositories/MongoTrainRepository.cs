using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities;
using TicketsDemo.Data.Repositories;
using TicketsDemo.Mongo.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using TicketsDemo.Mongo.Implementations;

namespace TicketsDemo.Mongo.Repositories
{
    public class MongoTrainRepository : ITrainRepository
    {
        ICollectionFactory _cf;

        public MongoTrainRepository(ICollectionFactory cf)
        {
            _cf = cf;
        }

        public void CreateTrain(Train train)
        {
            ICollectionCreator cc = _cf.CreateCollection();
            MongoTrain mTrain = new MongoTrain
            {
                Carriages = train.Carriages,
                StartLocation = train.StartLocation,
                EndLocation = train.EndLocation,
                TrainId = train.Id,
                Number = train.Number
            };
            cc.Trains.InsertOne(mTrain);
        }

        public void DeleteTrain(Train train)
        {
            //ICollectionCreator cc = _cf.CreateCollection();
            //cc.Trains.DeleteOne(new BsonDocument("_id", train.Id));

            IMongoCollection<MongoTrain> trains = _cf.CreateCollection<MongoTrain>("MongoTrain");
            trains.DeleteOne(new BsonDocument("_id", train.Id));

            //IMongoCollection<MongoTrain> trains = _cf.CreateCollection<Train>("nameOfCollection");
            //trains.DeleteOne..... фактически пропадет CollectionCreator
        }

        public List<Train> GetAllTrains()
        {
            ICollectionCreator cc = _cf.CreateCollection();
            return cc.Trains.Find(train => true).ToList().Select(mt => new Train() {
                Carriages = mt.Carriages,
                StartLocation = mt.StartLocation,
                EndLocation = mt.EndLocation,
                Id = mt.TrainId,
                Number = mt.Number
            }).ToList();
        }

        public Train GetTrainDetails(int trainId)
        {
            ICollectionCreator cc = _cf.CreateCollection();
            var mTrain = cc.Trains.Find(new BsonDocument("TrainId", trainId)).FirstOrDefault();
            Train train = new Train()
            {
                Carriages = mTrain.Carriages,
                StartLocation = mTrain.StartLocation,
                EndLocation = mTrain.EndLocation,
                Id = mTrain.TrainId,
                Number = mTrain.Number
            };

            foreach (var car in train.Carriages) {
                car.Train = train;
                foreach (var place in car.Places)
                {
                    place.Carriage = car;
                }
            };

            return train;
        }

        public void UpdateTrain(Train train)
        {
            ICollectionCreator cc = _cf.CreateCollection();
            MongoTrain mTrain = new MongoTrain()
            {
                Carriages = train.Carriages,
                StartLocation = train.StartLocation,
                EndLocation = train.EndLocation,
                TrainId = train.Id,
                Number = train.Number
            };
            cc.Trains.ReplaceOneAsync(new BsonDocument("_id", train.Id), mTrain);
        }
    }
}
