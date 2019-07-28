using ChangeLogWeb.Domain;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChangeLogWeb.Repository
{
    public class TeamRepository
    {
        private IMongoClient Client { get; set; }
        private IMongoDatabase Database { get; set; }
        private IMongoCollection<Team> collection { get; set; }

        public TeamRepository()
        {
            Client = new MongoClient("mongodb://localhost");
            Database = Client.GetDatabase("changelog-web");
            collection = Database.GetCollection<Team>("team");
        }

        public void Insert(Team team)
        {
            team.Id = ObjectId.GenerateNewId();
            collection.InsertOne(team);
        }
    }
}
